using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaBankImplementation
{
    public class Client : Person
    {
        public decimal Solde;
        public string Reclamation { get; set; }
        public Credit Credit;
        public Client() { }
    }
}
