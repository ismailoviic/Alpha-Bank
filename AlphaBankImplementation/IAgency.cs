using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaBankImplementation
{
    public interface IAgency
    {
        bool RecruitSalesAgents(Employee employee, List<Employee> Salers);
        bool RespondToComplaints(Employee employee, Client client, string Respond);
        bool CreditAllocation(Employee employee, Client client, Offer offer);
        bool PurchaseEquipment(Employee employee, decimal amount);
        bool SalaryTransfer(Employee employee1, Employee employee, decimal salary);
        bool WithdrawalAmount(Employee employee, Client client, decimal amount);
        bool NewCreditOffer(Employee employee, Offer offer);
        bool CreateClientAccount(Employee employee, Client client);
        bool Transfers(Employee employee, Client client, decimal amount);
        bool Transaction(Employee employee, Client sender, Client receiver, decimal amount, TransactionType transaction);
    }
}
