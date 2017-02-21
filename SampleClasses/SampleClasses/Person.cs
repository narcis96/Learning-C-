using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleClasses
{
    namespace PersonNamespace
    {
        class PersonClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public PersonClass()
            {

            }
            public PersonClass(PersonClass P)
            {
                this.Name = P.Name;
                this.Age = P.Age;
            }
            public override string ToString()
            {
                return "Person: " + Name + " " + Age;
            }
        }
    }
    
}
