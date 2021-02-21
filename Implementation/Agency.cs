using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Agency : IAgency
    {
        public List<Employee> Employees = new List<Employee>();

        public List<Client> Clients = new List<Client>();
        public List<Credit> CreditOffers = new List<Credit>();
        public decimal Caisse { get; set; }

        const decimal AccountCreationFees = 30;
        const decimal NationalTransactionFees = 6;
        const decimal InternationalTransactionFees = 10;

        public Agency(List<Employee> employees, decimal caisse)
        {
            Employees = employees;
            Caisse = caisse;
        }

        public bool RecruitSalesAgents(Employee person, List<Employee> Salers)
        {
            if (person.Role == Role.Chef) { Employees.AddRange(Salers); return true; }
            return false;
        }

        public bool RespondToComplaints(Employee person, Client client, string Respond)
        {
            if (person.Role == Role.Chef) { client.Reclamation = Respond; return true; }
            return false;
        }

        public bool CreditAllocation(Employee person, Client client, decimal amount, int durationInMonth = 12, decimal monthlyInstallment = 0)
        {
            var clientHasNoCredit = client.Credit.Amount == 0;
            var compatibleOfferExist = CreditOffers.Exists(x =>
                x.Amount >= amount && (x.DurationInMonth >= durationInMonth || x.MonthlyInstallment <= monthlyInstallment));
            var amountExistInCaisse = Caisse > amount;
            if (person.Role == Role.Chef && clientHasNoCredit && compatibleOfferExist && amountExistInCaisse)
            {
                var compatibleOffer = CreditOffers.Find(x =>
                    x.Amount >= amount && x.DurationInMonth >= durationInMonth && x.MonthlyInstallment <= monthlyInstallment);

                client.Credit.Amount = compatibleOffer.Amount;
                client.Credit.DurationInMonth = compatibleOffer.DurationInMonth;
                client.Credit.InterestRate = compatibleOffer.InterestRate;
                client.Credit.MonthlyInstallment = compatibleOffer.MonthlyInstallment;

                Caisse -= amount;
                return true;
            }
            return false;
        }

        public bool PurchaseEquipment(Employee person, decimal amount)
        {
            var amountExistInCaisse = Caisse >= amount;
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.AdministrativeResponsible;
            if (amountExistInCaisse && theOneInCharge) { Caisse -= amount; return true; }
            return false;
        }

        public bool SalaryTransfer(Employee person, Employee employee, decimal salary)
        {
            var amountExistInCaisse = Caisse >= salary;
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.AdministrativeResponsible;
            if (amountExistInCaisse && theOneInCharge) { Caisse -= salary; employee.Solde += salary; return true; }
            return false;
        }

        public bool WithdrawalAmount(Employee person, Client client, decimal amount)
        {
            var amountExistSolde = Caisse >= amount;
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.AdministrativeResponsible;
            if (amountExistSolde && theOneInCharge) { client.Solde -= amount; return true; }
            return false;
        }

        public bool NewCreditOffer(Employee person, decimal amount, int duration, decimal interestRate)
        {
            var creditOfferExist = CreditOffers.Exists(cr => cr.Amount == amount && cr.DurationInMonth == duration && cr.InterestRate == interestRate);
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.ProductsResponsible;
            if (!creditOfferExist && theOneInCharge) { CreditOffers.Add(new Credit(amount, duration, interestRate)); return true; }
            return false;
        }

        public bool CreateAccount(Employee person, Person newClient, decimal solde)
        {
            var clientExist = Clients.Exists(cl => cl.CIN == newClient.CIN);
            var soldeSufficient = solde >= 30;
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.ProductsResponsible;
            if (!clientExist && theOneInCharge && soldeSufficient) { Clients.Add(new Client(newClient, solde - 30)); Caisse += 30; return true; }
            return false;
        }

        public bool Transfers(Employee person, Client client, decimal amount)
        {
            var clientExist = Clients.Exists(cl => cl.CIN == client.CIN);
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.ProductsResponsible || person.Role == Role.AdministrativeResponsible || person.Role == Role.CommercialAgent;
            if (clientExist && theOneInCharge) { client.Solde += amount; return true; }
            return false;
        }

        public bool Transaction(Employee person, Client sender, Client receiver, decimal amount, TransactionType transaction)
        {
            var transactionFees = transaction == TransactionType.National ? 6 : 30;
            var senderAndReceiverExist = Clients.Exists(cl => cl.CIN == sender.CIN) && Clients.Exists(cl => cl.CIN == receiver.CIN);
            var senderHasAmount = sender.Solde >= amount + transactionFees;
            var theOneInCharge = person.Role == Role.Chef || person.Role == Role.ProductsResponsible || person.Role == Role.AdministrativeResponsible || person.Role == Role.CommercialAgent;
            if (senderAndReceiverExist && senderHasAmount && theOneInCharge)
            {
                sender.Solde -= amount + transactionFees;
                receiver.Solde += amount;
                Caisse += transactionFees;
                return true;
            }
            return false;
        }
    }
}
