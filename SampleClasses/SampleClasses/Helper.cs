using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SampleClasses
{
    class Helper:IDisposable
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

        public void Dispose()
        {
            Console.WriteLine("Helper::Dispose");
        }
    }
}
