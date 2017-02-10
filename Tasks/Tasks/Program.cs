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
            Action finishFilesTask = () =>
            {
                Task<string[]> taskListFiles = Task.Run(() =>
                {
                    return Directory.GetFiles(path);
                });
                taskListFiles.ContinueWith((antecedent) =>
                {
                    string[] files = antecedent.Result;
                    foreach (string file in files)
                    {
                        lock (listFiles.SyncRoot)
                        {
                            listFiles.Add(file);
                        }
                    }
                }).Wait();
            };

            Action finishDirectoryTask = () =>
            {
                Func<string[]> funcListDiretories = () =>
                {
                    return Directory.GetDirectories(path);
                };
                Task<string[]> taskListDiretories = Task.Run(funcListDiretories);
                taskListDiretories.ContinueWith((antecedent) =>
                {
                    string[] directories = antecedent.Result;
                    foreach (string directory in directories)
                    {
                        ListDiretories(directory);
                    }
                }).Wait();
            };

            Parallel.Invoke(finishFilesTask, finishDirectoryTask);
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
            Func<int> getOne = () => 1;

            Func<int, int, string> convertIntToString = (i, j) => 
            i.ToString() + " + " + j.ToString()+ " = " + (i + j).ToString();

            Action<string> printToScreen = s => 
            {
                Console.WriteLine(s);
            };
            printToScreen(convertIntToString(getOne(),getOne()));
        }
    }
}
