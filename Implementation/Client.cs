using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Client : Person
    {
        public decimal Solde;
        public string Reclamation { get; set; }
        public Credit Credit;
        public Client(Person person, decimal solde)
            : base(person.FirstName, person.LastName, person.CIN)
        {
            Solde = solde;
            Credit = new Credit(0, 0, 0);
        }
        public Client(string fisrtName, string lastName, string cin, decimal solde)
            : base(fisrtName, lastName, cin)
        {
            Solde = solde;
            Credit = new Credit(0, 0, 0);
        }
    }
}
