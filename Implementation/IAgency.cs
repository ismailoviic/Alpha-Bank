using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public interface IAgency
    {
        bool RecruitSalesAgents(Employee person, List<Employee> Salers);
        bool RespondToComplaints(Employee person, Client client, string Respond);
        bool CreditAllocation(Employee person, Client client, decimal amount, int durationInMonth = 12, decimal monthlyInstallment = 0);
        bool PurchaseEquipment(Employee person, decimal amount);
        bool SalaryTransfer(Employee person, Employee employee, decimal salary);
        bool WithdrawalAmount(Employee person, Client client, decimal amount);
        bool NewCreditOffer(Employee person, decimal amount, int duration, decimal interestRate);
        bool CreateAccount(Employee person, Person newClient, decimal solde);
        bool Transfers(Employee person, Client client, decimal amount);
        bool Transaction(Employee person, Client sender, Client receiver, decimal amount, TransactionType transaction);
    }
}
