using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaBankImplementation
{
    public class Agency 
    {
        public List<Employee> Employees { get; set; }
        public List<Client> Clients { get; set; }
        public List<Offer> CreditOffers { get; set; }
        public decimal Caisse { get; set; }
    }
}
