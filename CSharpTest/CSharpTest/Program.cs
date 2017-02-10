using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSharpTest
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

        static void Func(ref int x)
        {
            x = 0;
        }
        static void Main(string[] args)
        {
            int x = 2;
            Func(ref x);
            Console.WriteLine(x);
        }
    }
}
