﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaBankImplementation
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CIN { get; set; }

        public bool ComparePersonTo(Person person)
        {
            return FirstName == person.FirstName &&
                LastName == person.LastName &&
                CIN == person.CIN;
        }

    }
}
