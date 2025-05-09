using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace FTP
{
    public class Client
    {
        private object sendLock = new object();

        private TcpListener messageListener;
        private TcpListener fileListener;
        private CancellationTokenSource cts;
        private Thread tMessage, tFiles;

        public string ipDestination;
        public int messagePortL;
        public int filePortL;
        public int messagePortS;
        public int filePortS;

        public void StartListening()
        {
            cts = new CancellationTokenSource();
            tMessage = new Thread(() => RunListenMessages(cts.Token));
            tFiles = new Thread(() => RunListenFiles(cts.Token));
            tMessage.Start();
            tFiles.Start();
        }

        public void StopListening()
        {
            cts?.Cancel();
            messageListener?.Stop();
            fileListener?.Stop();
        }

        private void RunListenMessages(CancellationToken token)
        {
            try
            {
                messageListener = new TcpListener(IPAddress.Any, messagePortL);
                messageListener.Start();
                while (!token.IsCancellationRequested)
                {
                    if (messageListener.Pending())
                    {
                        using (TcpClient client = messageListener.AcceptTcpClient())
                        using (NetworkStream stream = client.GetStream())
                        using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true))
                        {
                            int messageLength = reader.ReadInt32();
                            byte[] data = reader.ReadBytes(messageLength);
                            string receivedMessage = Encoding.UTF8.GetString(data);
                            OnMessageReceived(receivedMessage);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                OnMessageReceived($"Error: {ex.Message}");
            }
            finally
            {
                messageListener?.Stop();
            }
        }

        private void RunListenFiles(CancellationToken token)
        {
            try 
            {
                fileListener = new TcpListener(IPAddress.Any, filePortL);
                fileListener.Start();
                while (!token.IsCancellationRequested)
                {
                    if (fileListener.Pending())
                    {
                        using (TcpClient client = fileListener.AcceptTcpClient())
                        using (NetworkStream stream = client.GetStream())
                        using (FileStream fileStream = new FileStream(@".\PACS.zip", FileMode.Create))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fileStream.Write(buffer, 0, bytesRead);
                            }
                            OnFileReceived(@".\PACS.zip");
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch(Exception ex)
            {
                OnFileReceived($"Error: {ex.Message}");
            }
            finally
            {
                fileListener?.Stop();
            }
        }

        public void SendEncryptedCredentials(string key, string iv, string pdf)
        {
            Thread tKey = new Thread(SendCredential);
            Thread tIv = new Thread(SendCredential);
            Thread tPdf = new Thread(SendCredential);

            tKey.Start(key);
            tKey.Join();
            tIv.Start(iv);
            tIv.Join();
            tPdf.Start(pdf);
        }

        public void SendMessage(string message)
        {
            Thread senderThread = new Thread(SendMessageTCP);
            senderThread.Start(message);
        }
        private void SendMessageTCP(object message)
        {
            try
            {
                using (TcpClient client = new TcpClient(ipDestination, messagePortS))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    byte[] data = Encoding.UTF8.GetBytes(message.ToString());
                    writer.Write(data.Length);
                    writer.Write(data);
                }
            }
            catch (Exception ex)
            {
                OnMessageReceived($"Error enviant: {ex.Message}");
            }
        }

        public void SendCredential(object obj)
        {
            try
            {
                using (TcpClient client = new TcpClient(ipDestination, filePortS))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Convert.FromBase64String(obj.ToString());
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                OnFileReceived($"Error enviant: {ex.Message}");
            }
        }

        public class MessageEventArgs : System.EventArgs
        {
            public string msg;

        }

            
        public event EventHandler MessageReceived;

        protected virtual void OnMessageReceived(string msg)
        {
            if (null != MessageReceived)
            {
                MessageEventArgs e = new MessageEventArgs();
                e.msg = msg;
                MessageReceived(this, e);
            }

        }
        public class FileMessageEventArgs : EventArgs
        {
            public long Size { get; set; }
            public string FileName { get; set; }
        }

        public event EventHandler FileReceived;

        protected virtual void OnFileReceived(string filePath)
        {
            if(null != FileReceived)
            {
                FileMessageEventArgs e = new FileMessageEventArgs();
                e.FileName = filePath;
                FileReceived(this, e);
            }
        }
    }

}
