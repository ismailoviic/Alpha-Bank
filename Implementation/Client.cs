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
        public Client() { }
    }
}
