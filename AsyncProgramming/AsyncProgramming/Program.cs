using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace AsyncProgramming
{
    class Program
    {
        static async Task<string> ReadAsync(string filename)
        {
            await Task.Delay(2000);
            using (FileStream SourceStream = File.Open(filename, FileMode.Open))
            {
                long length = SourceStream.Length;
                byte[] byteArray = new byte[length];
                await SourceStream.ReadAsync(byteArray, 0, (int)length);
                string result = System.Text.Encoding.UTF8.GetString(byteArray);

                int threadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(threadID);

                return result;
            }
        }

        static void Main(string[] args)
        {
           Task<string> task = Program.ReadAsync(args[0]);

            int threadID = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(threadID);
            task.ContinueWith( (previous) =>
            {
                string result = previous.Result;
                Console.WriteLine(result);
            });
            Console.WriteLine(threadID);
            task.Wait();
        }
    }
}
