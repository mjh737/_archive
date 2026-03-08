using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Person BuysFor { get; set; }

        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
