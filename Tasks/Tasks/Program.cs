using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections;
namespace Tasks
{
    class Program
    {
        static void DoStuff()
        {
            Console.WriteLine("DoStuff");
        }
        static ArrayList listFiles = new ArrayList();
        static void ListDiretories(String path)
        {
            Task[] tasks = new Task[2];
            tasks[0] = Task.Run(() =>
            {
                int threadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(threadID + " task[0]");
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                    lock (listFiles.SyncRoot)
                    {
                        listFiles.Add(file);
                    }
                }
            });
            tasks[1] = Task.Run(() =>
            {
                int threadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine(threadID + " task[1]");
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    ListDiretories(directory);
                }
            });
            Task.WaitAll(tasks);
        }

        static void Main(string[] args)
        {
            ListDiretories(args[0]);
            listFiles.Sort();
            foreach(string file in listFiles)
            {
                Console.WriteLine(file);
            }
            Action doStuff = DoStuff;
            Func<int> getOne = () => 
            {
                return 2 + 1;
            };

            Func<int, int, string> convertIntToString = (i, j) =>
            {
                return i.ToString() + " + " + j.ToString() + " = " + (i + j).ToString();
            };

            Action<string> printToScreen = s => 
            {
                Console.WriteLine(s);
            };
            printToScreen(convertIntToString(getOne(),getOne()));
        }
    }
}
