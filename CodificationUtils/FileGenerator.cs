using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;


namespace CodificationUtils
{
    public class FileGenerator
    {
        readonly object sumTotalLock = new object();
        long sumTotal;

        public EncodingConfig config { get; set; }
        public Dictionary<string, string> codifications { get; set; }

        Thread tEncoded, tCount;

        public void EncodeFilesAndSum()
        {
            UnZipFiles($"./PACS.zip");
            tEncoded = new Thread(GenerateEncodedFiles);
            tCount = new Thread(SumTotal);

            tCount.Start();
            tEncoded.Start();
        }   

        public void UnZipFiles(string zipFile)
        {
            MyFileUtils.CreateOrEmptyDirectory(config.OriginalFilesPath);

            try
            {
                ZipFile.ExtractToDirectory(zipFile, config.OriginalFilesPath);
            }
            catch (Exception) { }
        }

        private void GenerateEncodedFiles()
        {
            MyFileUtils.CreateOrEmptyDirectory(config.EncodedFilesPath);
            Parallel.For(0, config.NumFiles,
                index =>
                {
                    string originalPath = $"{config.OriginalFilesPath}fitxer{index + 1}";
                    string encodedPath = $"{config.EncodedFilesPath}fitxer{index + 1}";
                    ReadAndWriteCodification(originalPath, encodedPath);
                });
        }

        private void SumTotal()
        {
            if (tEncoded != null && tEncoded.ThreadState != ThreadState.Unstarted)
            {
                tEncoded.Join();
            }
            sumTotal = 0;
            Parallel.For(0, config.NumFiles,
                index =>
                {
                    string encodedPath = $"{config.EncodedFilesPath}fitxer{index + 1}";
                    long sumFile = ReadAndSum(encodedPath);
                    lock (sumTotalLock)
                    {
                        sumTotal += sumFile;
                    }

                });

            OnSumFinished(sumTotal);
        }

        private void ReadAndWriteCodification(string originalPath, string encodedPath)
        {
            char letter;
            using (FileStream iStream = new FileStream($"{originalPath}.txt", FileMode.Open))
            using (FileStream oStream = new FileStream($"{encodedPath}.txt", FileMode.Create))
            using (StreamReader sr = new StreamReader(iStream))
            using (StreamWriter sw = new StreamWriter(oStream))
            {
                while ((letter = (char)sr.Read()) != '\r')
                {
                    sw.Write(codifications[letter.ToString()]);
                }
                sw.WriteLine();
            }
        }

        private long ReadAndSum(string filePath)
        {
            long sum = 0;
            int number;
            using (FileStream iStream = new FileStream($"{filePath}.txt", FileMode.Open))
            using (StreamReader sr = new StreamReader(iStream))
            {
                while ((number = sr.Read()) != '\r')
                {
                    if (number == 0) number = 10;
                    sum += number;
                }
            }
            return sum;
        }

        public class SumFinishedEventArgs : System.EventArgs
        {
            public long sum;
        }

        public event EventHandler SumFinished;

        protected virtual void OnSumFinished(long sum)
        {
            if (null != SumFinished)
            {
                SumFinishedEventArgs e = new SumFinishedEventArgs();
                e.sum = sum;
                SumFinished(this, e);
            }
        }

    }
}
