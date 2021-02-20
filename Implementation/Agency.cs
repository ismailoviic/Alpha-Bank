using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Agency : IAgency
    {
        public List<Employee> Employees = new List<Employee>();

        public List<Client> Clients = new List<Client>();
        public decimal Caisse { get; set; }

        const decimal AccountCreationFees = 30;
        const decimal NationalTransactionFees = 6;
        const decimal InternationalTransactionFees = 10;

        public Agency(List<Employee> employees, decimal caisse) {
            Employees = employees;
            Caisse = caisse;
        }
    }
}
