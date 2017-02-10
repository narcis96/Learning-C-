using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ParallelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Action firstAction = () =>
            {
                Task task = Task.Run(() =>
                {
                    int threadID = Thread.CurrentThread.ManagedThreadId;
                    for (int i = 0; i < 5; ++i)
                    {
                        Console.WriteLine("ThreadProc " + threadID + " : " + i.ToString());
                        Thread.Sleep(1000);
                    }
                });
                task.Wait();
            };
            Action secondAction = () =>
            {
                Task task = Task.Run(() =>
                {
                    int threadID = Thread.CurrentThread.ManagedThreadId;
                    for (int i = 0; i < 10; ++i)
                    {
                        Console.WriteLine("ThreadProc " + threadID + " : " + i.ToString());
                        Thread.Sleep(500);
                    }
                });
                task.Wait();
            };
            Parallel.Invoke(firstAction, secondAction);
        }
    }
}
