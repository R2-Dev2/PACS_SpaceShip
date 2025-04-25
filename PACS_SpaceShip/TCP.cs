using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace PACS_SpaceShip
{
    class TCP
    {
        private const int BufferSize = 8192; // 8 KB
        private TcpListener listener;
        private CancellationTokenSource cts;
        private Thread serverThread;

        public string ip;
        public int listenPort;
        public int sendPort;
        private string messageToSend;

        public event EventHandler<MessageEventArgs> MessageReceived;

        public TCP(string ip, int listenPort, int sendPort)
        {
            this.ip = ip;
            this.listenPort = listenPort;
            this.sendPort = sendPort;
        }

        public void SetMessage(string message)
        {
            messageToSend = message;
        }

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
                listener = new TcpListener(IPAddress.Parse(ip), listenPort);
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

        public void StartSendMessage()
        {
            Thread senderThread = new Thread(SendMessage);
            senderThread.Start();
        }

        private void SendMessage()
        {
            try
            {
                using (TcpClient client = new TcpClient(ip, sendPort))
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

        protected virtual void OnMessageReceived(string msg)
        {
            MessageReceived?.Invoke(this, new MessageEventArgs { MsgContent = msg });
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public string MsgContent { get; set; }
    }
}
