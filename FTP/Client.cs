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
        private const int BufferSize = 8192; // 8 KB
        private TcpListener listener;
        private CancellationTokenSource cts;
        private Thread serverThread;

        public string ipDestination;
        public int listenPort;
        public int sendPort;
        private string messageToSend;


        public void StartListening()
        {
            cts = new CancellationTokenSource();
            serverThread = new Thread(() => RunServer(cts.Token));
            serverThread.Start();
        }

        public void StopListening()
        {
            cts?.Cancel();
            listener?.Stop();
        }

        private void RunServer(CancellationToken token)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, listenPort);
                listener.Start();
                while (!token.IsCancellationRequested)
                {
                    if (listener.Pending())
                    {
                        using (TcpClient client = listener.AcceptTcpClient())
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
                listener?.Stop();
            }
        }

        public void SendMessage(string message)
        {
            this.messageToSend = message;

            Thread senderThread = new Thread(SendMessageTCP);
            senderThread.Start();
        }

        public void SendMessage(string message, string lenght)
        {
            this.messageToSend = message;

            Thread senderThread = new Thread(SendMessageTCP);
            senderThread.Start();
        }

        private void SendMessageTCP()
        {
            try
            {
                using (TcpClient client = new TcpClient(ipDestination, sendPort))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    byte[] data = Encoding.UTF8.GetBytes(messageToSend);
                    writer.Write(data.Length);
                    writer.Write(data);
                }
            }
            catch (Exception ex)
            {
                OnMessageReceived($"Error enviant: {ex.Message}");
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
    }

}
