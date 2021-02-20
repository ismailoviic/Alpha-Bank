using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CIN { get; set; }

        public Person(string fisrtName, string lastName, string cin)
        {
            FirstName = fisrtName;
            LastName = lastName;
            CIN = cin;
        }

        public bool ComparePersonTo(Person person)
        {
            return FirstName == person.FirstName &&
                LastName == person.LastName &&
                CIN == person.CIN;
        }

    }
}
