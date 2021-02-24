using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaBankImplementation
{
    public class Agency : IAgency
    {
        public List<Employee> Employees { get; set; }

        public List<Client> Clients { get; set; }
        public List<Offer> CreditOffers { get; set; }
        public decimal Caisse { get; set; }

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

        public bool RespondToComplaints(Employee employee, Client client, string Respond)
        {
            if (employee.Role == Role.Chef) { client.Reclamation = Respond; return true; }
            return false;
        }

        public bool CreditAllocation(Employee employee, Client client, Offer offer)
        {
            var clientHasNoCredit = client.Credit == null;
            var compatibleOffer = CreditOffers.First(x => x.Name.Equals(offer.Name));

            var amountExistInCaisse = Caisse > offer.Amount;
            if (!amountExistInCaisse) return false;
            if (compatibleOffer == null) return false;
            if (employee.Role == Role.Chef && clientHasNoCredit)
            {
                client.Credit = new Credit
                {
                    Amount = compatibleOffer.Amount,
                    DurationInMonth = compatibleOffer.DurationInMonth,
                    InterestRate = compatibleOffer.InterestRate,
                    MonthlyInstallment = compatibleOffer.MonthlyInstallment
                };
                Caisse -= offer.Amount;
                return true;
            }
            return false;
        }

        public bool PurchaseEquipment(Employee employee, decimal amount)
        {
            var amountExistInCaisse = Caisse >= amount;
            var theOneInCharge = employee.Role == Role.AdministrativeResponsible;
            if (amountExistInCaisse && theOneInCharge) { Caisse -= amount; return true; }
            return false;
        }

        public bool SalaryTransfer(Employee employee1, Employee employee, decimal salary)
        {
            var amountExistInCaisse = Caisse >= salary;
            var theOneInCharge = employee1.Role == Role.AdministrativeResponsible;
            if (amountExistInCaisse && theOneInCharge) { Caisse -= salary; employee.Solde += salary; return true; }
            return false;
        }

        public bool WithdrawalAmount(Employee employee, Client client, decimal amount)
        {
            var amountExistSolde = Caisse >= amount;
            var theOneInCharge = employee.Role == Role.AdministrativeResponsible;
            if (amountExistSolde && theOneInCharge) { client.Solde -= amount; return true; }
            return false;
        }

        public bool NewCreditOffer(Employee employee, Offer offer)
        {
            var creditOfferExist = CreditOffers.Exists(cr => cr.Name.Equals(offer.Name));
            if (creditOfferExist) return false;
            if (employee.Role == Role.ProductsResponsible)
            {
                CreditOffers.Add(offer);
                return true;
            }
            return false;
        }

        public bool CreateClientAccount(Employee employee, Client newClient)
        {
            var clientExist = Clients.Exists(cl => cl.CIN == newClient.CIN);
            var soldeSufficient = newClient.Solde >= 30;
            if (employee.Role != Role.ProductsResponsible) return false;
            if (clientExist) return false;
            if (soldeSufficient)
            {
                newClient.Solde -= 30;
                Clients.Add(newClient);
                Caisse += 30;
                return true;
            }
            return false;
        }
        public bool Transfers(Employee employee, Client client, decimal amount)
        {
            var clientExist = Clients.Exists(cl => cl.CIN == client.CIN);
            if (!clientExist || employee.Role != Role.CommercialAgent) return false;
            client.Solde += amount; return true;
        }

        public bool Transaction(Employee employee, Client sender, Client receiver, decimal amount, TransactionType transaction)
        {
            var transactionFees = transaction == TransactionType.National ? 6 : 30;
            var senderAndReceiverExist = Clients.Select(cl => cl.CIN).Intersect(new[] { sender.CIN, receiver.CIN }).Count() == 2;
            var senderHasAmount = sender.Solde >= amount + transactionFees;
            var theOneInCharge = employee.Role == Role.CommercialAgent;
            if (!senderAndReceiverExist || !senderHasAmount || !theOneInCharge) return false;
            sender.Solde -= amount + transactionFees;
            receiver.Solde += amount;
            Caisse += transactionFees;
            return true;
        }
    }
}
