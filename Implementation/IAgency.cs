using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public interface IAgency
    {
        bool RecruitSalesAgents(Employee person, List<Employee> Salers);
        bool RespondToComplaints(Employee person, Client client, string Respond);
        bool CreditAllocation(Employee person, Client client, Credit credit);
        bool PurchaseEquipment(Employee person, decimal amount);
        bool SalaryTransfer(Employee person, Employee employee, decimal salary);
        bool WithdrawalAmount(Employee person, Client client, decimal amount);
        bool NewCreditOffer(Employee person, decimal amount, int duration, decimal interestRate);
        ResponseModel CreateClientAccount(Employee person, Client newClient);
        bool Transfers(Employee employee, Client client, decimal amount);
        bool Transaction(Employee person, Client sender, Client receiver, decimal amount, TransactionType transaction);
    }
}
