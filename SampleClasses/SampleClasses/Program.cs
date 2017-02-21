using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SampleClasses
{
    using Person = PersonNamespace.PersonClass;//alias
    using static System.Console;//we can use WriteLine() insead of System.Console.WriteLine()

    class Program
    {
        static void WriteChange(Person person)
        {
            Person P2 = person;
            P2.Name = "Changed";
            WriteLine(P2);
        }
        static void WriteNotChange(Person person)
        {
            Person P2 = new Person(person);
            P2.Name = "NothingHappens";
            WriteLine(P2);
        }
        static void WriteNotChangeAgain(Person person)
        {
            person = new Person();
            person.Name = "NothingHappensAgain";
            WriteLine(person);
        }
        static void Write(int x)
        {
            x = 69;//primitive types are passed by value
            WriteLine(x);
        }

        static void Func(ref int x)
        {
            x = 0;//Now will be changed
        }

        static void Change(int[] pArray)
        {
            pArray[0] = 888;  // This change affects the original element.
            pArray = new int[5] { -3, -1, -2, -3, -4 };   // This change is local.
            WriteLine("Inside the method, the first element is: {0}", pArray[0]);
        }
        static void Change(ref int[] pArray)
        {
            // Both of the following changes will affect the original variables:
            pArray[0] = 888;
            pArray = new int[5] { -3, -1, -2, -3, -4 };
           WriteLine("Inside the method, the first element is: {0}", pArray[0]);
        }
        static void Main(string[] args)
        {
            int x = 10;
            Write(x);
            WriteLine(x);

            Func(ref x);
            WriteLine(x);

            Point<int> point = new Point<int>(2, 3);
            point.X = 4;
            WriteLine(point);

            Person person1 = new Person { Name = "NotChanged", Age = 18 };

            WriteNotChange(person1);
            WriteNotChangeAgain(person1);
            WriteLine("Inside Main :" + person1);

            WriteChange(person1);
            WriteLine("Inside Main :" + person1);

            int[] arr = { 1, 4, 5 };
            WriteLine("Inside Main, before calling the method, the first element is: {0}", arr[0]);

            Change(arr);
            WriteLine("Inside Main, after calling the method, the first element is: {0}", arr[0]);
            
            using (Helper helper = new Helper())
            {
                helper.DoStuff();
            }
        }
    }
}
