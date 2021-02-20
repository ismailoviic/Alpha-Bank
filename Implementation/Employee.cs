using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Employee : Person
    {
        public Role Role;
        public decimal Solde;
        public Employee(Person person, Role role, decimal solde)
    : base(person.FirstName, person.LastName, person.CIN)
        {
            Role = role;
            Solde = solde;
        }
    }
}
