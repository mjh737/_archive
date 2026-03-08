using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuddersAlgorithm
{
    public class Person
    {
        public string Name { get; set; }
        public Person BuysFor { get; set; }

        public Person(string name)
        {
            Name = name;
        }
    }
}
