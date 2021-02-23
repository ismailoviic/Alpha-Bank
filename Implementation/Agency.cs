using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Agency : IAgency
    {
        public List<Employee> Employees { get; set; }
        public List<Client> Clients = new List<Client>();
        public List<Offer> CreditOffers = new List<Offer>();
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

        public bool CreditAllocation(Employee person, Client client, Offer credit)
        {
            var clientHasNoCredit = client.Credit == null;
            var compatibleOffer = CreditOffers.First(x => x.Name.Equals(credit.Name));
            var amountExistInCaisse = Caisse > credit.Amount;
            if (!amountExistInCaisse) return false;
            if (compatibleOffer == null) return false;
            if (person.Role == Role.Chef && clientHasNoCredit)
            {
                client.Credit = new Credit
                {
                    Amount = compatibleOffer.Amount,
                    DurationInMonth = compatibleOffer.DurationInMonth,
                    InterestRate = compatibleOffer.InterestRate,
                    MonthlyInstallment = compatibleOffer.MonthlyInstallment
                };
                Caisse -= credit.Amount;
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
            if (!creditOfferExist && theOneInCharge)
            {
                CreditOffers.Add(new Offer
                {
                    Name = "string",
                    Amount = amount,
                    InterestRate = interestRate,
                    DurationInMonth = duration,
                    IsActive = true
                }); return true;
            }
            return false;
        }

        public ResponseModel CreateClientAccount(Employee person, Client newClient)
        {
            var clientExist = Clients.Exists(cl => cl.CIN == newClient.CIN);
            var soldeSufficient = newClient.Solde >= 30 ? newClient.Solde - 30 : newClient.Solde;
            if (person.Role != Role.ProductsResponsible) return new ResponseModel
            {
                Response = Response.Failed,
                Reason = "Agent Indisponible"
            };

            if (clientExist) return new ResponseModel
            {
                Response = Response.Failed,
                Reason = "Client existant"
            };

            if (soldeSufficient < 30) return new ResponseModel
            {
                Response = Response.Failed,
                Reason = "Solde inssufisant"
            };


            Clients.Add(newClient);
            Caisse += 30;
            return new ResponseModel
            {
                Response = Response.Success,
                Reason = "Operation completed"
            };
        }

        public bool Transfers(Employee employee, Client client, decimal amount)
        {
            var clientExist = Clients.Exists(cl => cl.CIN == client.CIN);
            if (!clientExist || employee.Role != Role.CommercialAgent) return false;
            client.Solde += amount; return true;
        }

        public bool Transaction(Employee person, Client sender, Client receiver, decimal amount, TransactionType transaction)
        {
            var transactionFees = transaction == TransactionType.National ? 6 : 30;
            var senderAndReceiverExist = Clients.Select(cl => cl.CIN).Intersect(new[] { sender.CIN, receiver.CIN }).Count() == 2;
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
