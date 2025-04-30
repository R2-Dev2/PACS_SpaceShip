using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FTP
{
    public static class FileTransferHelper
    {
        public static void SendFile(string filePath, string ip, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient(ip, port))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    string fileName = Path.GetFileName(filePath);
                    byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                    writer.Write(fileNameBytes.Length); 
                    writer.Write(fileNameBytes); 

                    long fileSize = new FileInfo(filePath).Length;
                    writer.Write(fileSize);

                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[4096]; 
                        int bytesRead;
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviant el fitxer: {ex.Message}");
            }
        }

        public static void StartFileReceiver(string ip, int port, string saveDirectory, Action<string> onFileReceived)
        {
            Thread receiverThread = new Thread(() =>
            {
                TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
                listener.Start();

                while (true)
                {
                    using (TcpClient client = listener.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true))
                    {
                        if (stream.DataAvailable) 
                        {
                            int fileNameLength = reader.ReadInt32();
                            string fileName = Encoding.UTF8.GetString(reader.ReadBytes(fileNameLength));
                            long fileSize = reader.ReadInt64();

                            string filePath = Path.Combine(saveDirectory, fileName);
                            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            {
                                byte[] buffer = new byte[4096];
                                long bytesReceived = 0;

                                while (bytesReceived < fileSize)
                                {
                                    int bytesToRead = (int)Math.Min(buffer.Length, fileSize - bytesReceived);
                                    int read = stream.Read(buffer, 0, bytesToRead);
                                    fs.Write(buffer, 0, read);
                                    bytesReceived += read;
                                }
                            }
                        }
                    }
                }
            });

            receiverThread.IsBackground = true;
            receiverThread.Start();
        }
    }
}


