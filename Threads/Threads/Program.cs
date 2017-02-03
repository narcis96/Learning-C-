using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threads
{
    class Helper
    {
        public Helper()
        {
            Console.WriteLine("Helper::Helper");
        }
        ~Helper()
        {
            Console.WriteLine("Helper::~Helper");
        }
        public void DoStuff()
        {

            Console.WriteLine("Helper::DoStuff");
        }
    }

    class Program
    {
        static private bool stopped;

        [ThreadStatic]
        static private int _field;

        public static void Func(object data)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Thread " + threadID + " starts");
            int n = (int)data;
            while (stopped == false)
            {
                for (int i = 0; i < n; i++)
                {
                    _field += 1;
                    Console.WriteLine("ThreadProc " + threadID + " : " + i.ToString());
                    Thread.Sleep(1000);
                }
            }
            Console.WriteLine("Thread " + threadID + " ends");

        }

        static void Main(string[] args)
        {
            stopped = false;
            {
                Helper h = new Helper();
                h.DoStuff();
                h = null;
            }
            Thread firstThread = new Thread(Program.Func);

            Thread secondThread = new Thread(new ThreadStart(() =>
            {
                int threadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Thread " + threadID + " starts");
                while (true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("ThreadProc " + threadID + " : " + i.ToString());
                        Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("Thread " + threadID + " ends");//never reach here
            }));

            Thread thirdThread = new Thread(new ParameterizedThreadStart((object data) =>
            {
                int threadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Thread " + threadID + " starts");
                int n = (int)data;
                //while (true)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine("ThreadProc " + threadID + " : " + i.ToString());
                        Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("Thread " + threadID + " ends");
            }));
            new Thread((object data) =>
            {
                int threadID = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("Thread " + threadID + " starts");
                int n = (int)data;
                for (int i = 0; i < n; i++)
                {
                    _field += 1;
                    Console.WriteLine("ThreadProc " + threadID + " : " + i.ToString());
                }
                Console.WriteLine("Thread " + threadID + " ends");
            }).Start(5);

            firstThread.Start(5);

            secondThread.IsBackground = true;
            secondThread.Start();

            thirdThread.Start(5);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            stopped = true;
            firstThread.Join();
            //secondThread.Join();
            thirdThread.Join();
            Thread.Sleep(3000);
            Console.WriteLine("_field = " + _field);
        }
    }
}
