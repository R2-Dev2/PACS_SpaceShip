using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTP
{
    public static class FileTransferHelper
    {
        private const int BufferSize = 8192;

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

                    byte[] fileData = File.ReadAllBytes(filePath);
                    writer.Write(fileData.Length);

                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[BufferSize];
                        int bytesRead;
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            writer.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviant arxiu: {ex.Message}");
            }
        }

        public static void StartFileReceiver(string ip, int port, string saveDirectory, Action<string> onFileReceived)
        {
            Thread receiverThread = new Thread(() =>
            {
                try
                {
                    TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
                    listener.Start();
                    Console.WriteLine("Esperant arxius...");

                    while (true)
                    {
                        using (TcpClient client = listener.AcceptTcpClient())
                        using (NetworkStream stream = client.GetStream())
                        using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true))
                        {
                            int fileNameLength = reader.ReadInt32();
                            string fileName = Encoding.UTF8.GetString(reader.ReadBytes(fileNameLength));

                            int totalFileSize = reader.ReadInt32();
                            string filePath = Path.Combine(saveDirectory, fileName);

                            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            {
                                byte[] buffer = new byte[BufferSize];
                                int bytesReceived = 0;
                                while (bytesReceived < totalFileSize)
                                {
                                    int bytesToRead = Math.Min(BufferSize, totalFileSize - bytesReceived);
                                    int read = stream.Read(buffer, 0, bytesToRead);
                                    fs.Write(buffer, 0, read);
                                    bytesReceived += read;
                                }
                            }

                            onFileReceived?.Invoke($"Arxiu rebut i desat a: {filePath}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    onFileReceived?.Invoke($"Error rebent arxiu: {ex.Message}");
                }
            });

            receiverThread.IsBackground = true;
            receiverThread.Start();
        }
    }
}
