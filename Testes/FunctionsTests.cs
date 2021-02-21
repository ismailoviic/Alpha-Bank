using Alpha_Bank.Implementation;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alpha_Bank.Testes
{
    public class FunctionsTests
    {
        public Agency NewAgency;
        public List<Client> Clients;
        public FunctionsTests()
        {
            var employees = new List<Employee>() {
                new Employee(new Person("Ismail","Essabbagh","AE202368"),Role.Chef,123456),
                new Employee(new Person("Aymen","Graoui","AE202888"),Role.AdministrativeResponsible,4851),
                new Employee(new Person("Yassine","Essebbagh","AE481528"),Role.ProductsResponsible,956),
                new Employee(new Person("Manal","Drissi","AE152638"),Role.CommercialAgent,1253),
                new Employee(new Person("Omar","Essabbagh","AE887568"),Role.CommercialAgent,1425),
                new Employee(new Person("Asmae","Alharaj","AE9865247"),Role.CommercialAgent,6254),
            };
            NewAgency = new Agency(employees, 99999999);
            var clients = new List<Client>() {
                new Client("Said","Mosker","AX96325",150),
                new Client("Samira","Salamat","AX97725",130),
                new Client("Rania","Romaisae","AX098765",1850),
                new Client("Rachid","Mohssin","AX96944",500),
                new Client("Kamal","Sadiki","AX98875",1750),
                new Client("Said","Sadiki","AX02525",158000),
            };
            NewAgency.Clients = clients;
            var NewCreditOffer = new List<Credit>() {
                new Credit(10000, 12, 3),
                new Credit(20000, 12, 3),
                new Credit(50000, 12, 3),
                new Credit(100000, 12, 3),
                new Credit(150000, 12, 3),
                new Credit(200000, 12, 3),
                new Credit(10000, 24, 4),
                new Credit(20000, 24, 4),
                new Credit(50000, 24, 4),
                new Credit(100000, 24, 4),
                new Credit(150000, 24, 4),
                new Credit(200000, 24, 4)
            };
            NewAgency.CreditOffers = NewCreditOffer;
        }

        [Fact]
        public void RecruitSalesAgents()
        {
            var BankAgency = NewAgency;
            var NewEmploye = new List<Employee>() { new Employee(new Person("Zakaria", "Essabbagh", "AE948157"), Role.CommercialAgent, 625) };
            var sut3 = NewEmploye.Count + NewAgency.Employees.Count;
            var chef = BankAgency.Employees.Find(emp => emp.Role == Role.Chef);
            var sut1 = BankAgency.RecruitSalesAgents(chef, NewEmploye);
            var sut2 = BankAgency.Employees.Exists(emp => emp.ComparePersonTo(NewEmploye.Find(em => em.CIN == "AE948157")));

            Assert.True(sut1);
            Assert.True(sut2);
            Assert.Equal(sut3, BankAgency.Employees.Count);

        }
        [Fact]
        public void RespondToComplaints()
        {
            var BankAgency = NewAgency;
            var client = BankAgency.Clients.Find(cl => cl.CIN == "AX96325");
            client.Reclamation = "why should i wait that long to get my salary?";
            var Respond = "we are working on speeding up our process but your company is late";
            var chef = BankAgency.Employees.Find(emp => emp.Role == Role.Chef);
            var sut = BankAgency.RespondToComplaints(chef, client, Respond);
            Assert.True(sut);
        }
        [Fact]
        public void CreditAllocation()
        {
            var BankAgency = NewAgency;
            var client = BankAgency.Clients.Find(cl => cl.CIN == "AX96325");
            var chef = BankAgency.Employees.Find(emp => emp.Role == Role.Chef);
            var sut = BankAgency.CreditAllocation(chef, client, 100000, 22, 5000);
            Assert.True(sut);
        }
        [Fact]
        public void PurchaseEquipment()
        {
            var BankAgency = NewAgency;
            var Admin = BankAgency.Employees.Find(emp => emp.Role == Role.AdministrativeResponsible);
            var amountOfPurchase = 5000;
            var caisse = BankAgency.Caisse;
            var sut = BankAgency.PurchaseEquipment(Admin, amountOfPurchase);
            Assert.True(sut);
            Assert.Equal(caisse - amountOfPurchase, BankAgency.Caisse);
        }
        [Fact]
        public void SalaryTransfer()
        {
            var BankAgency = NewAgency;
            var Admin = BankAgency.Employees.Find(emp => emp.Role == Role.AdministrativeResponsible);
            var employe = BankAgency.Employees.Find(emp => emp.Role == Role.Chef);
            var solde = employe.Solde;
            var salary = 20000;
            var caisse = BankAgency.Caisse;
            var sut = BankAgency.SalaryTransfer(Admin, employe, salary);
            Assert.True(sut);
            Assert.Equal(caisse - salary, BankAgency.Caisse);
            Assert.Equal(solde + salary, BankAgency.Employees.Find(emp => emp.Role == Role.Chef).Solde);
        }
        [Fact]
        public void WithdrawalAmount()
        {
            var BankAgency = NewAgency;
            var Admin = BankAgency.Employees.Find(emp => emp.Role == Role.AdministrativeResponsible);
            var client = BankAgency.Clients.Find(cl => cl.CIN == "AX02525");
            var solde = client.Solde;
            var amount = 6000;
            var sut = BankAgency.WithdrawalAmount(Admin, client, amount);
            Assert.True(sut);
            Assert.Equal(solde - amount, client.Solde);
        }
        [Fact]
        public void NewCreditOffer()
        {
            var BankAgency = NewAgency;
            var ProductResp = BankAgency.Employees.Find(emp => emp.Role == Role.ProductsResponsible);
            var sut1 = BankAgency.NewCreditOffer(ProductResp, 500000, 60, 5);
            var sut2 = BankAgency.NewCreditOffer(ProductResp, 100000, 12, 3);
            Assert.True(sut1);
            Assert.False(sut2);
        }
        [Fact]
        public void CreateAccount()
        {
            var BankAgency = NewAgency;
            var ProductResp = BankAgency.Employees.Find(emp => emp.Role == Role.ProductsResponsible);
            var newClient = new Person("Taha", "Chakir", "AW845123");
            var solde = 1000;
            var sut1 = BankAgency.CreateAccount(ProductResp, newClient, solde);
            var sut2 = BankAgency.Clients.Exists(cli => cli.CIN == "AW845123");
            var sut3 = BankAgency.Clients.Find(cli => cli.CIN == "AW845123").Solde;
            Assert.True(sut1);
            Assert.True(sut2);
            Assert.Equal(solde - 30, sut3);
        }
        [Fact]
        public void Transfers()
        {
            var BankAgency = NewAgency;
            var Commercial = BankAgency.Employees.Find(emp => emp.Role == Role.CommercialAgent);
            var client = BankAgency.Clients.Find(cl => cl.CIN == "AX96944");
            var amount = 6580;
            var solde = client.Solde;
            var sut = BankAgency.Transfers(Commercial, client, amount);
            Assert.True(sut);
            Assert.Equal(solde + amount, BankAgency.Clients.Find(cl => cl.CIN == "AX96944").Solde);

        }
        [Fact]
        public void Transaction()
        {
            var BankAgency = NewAgency;
            var Commercial = BankAgency.Employees.Find(emp => emp.Role == Role.CommercialAgent);
            var client1 = BankAgency.Clients.Find(cl => cl.CIN == "AX02525");
            var client2 = BankAgency.Clients.Find(cl => cl.CIN == "AX96944");
            var client3 = BankAgency.Clients.Find(cl => cl.CIN == "AX97725");
            var amount = 7000;
            var solde1 = client1.Solde;
            var solde2 = client2.Solde;
            var sut1 = BankAgency.Transaction(Commercial, client1, client2, amount, TransactionType.National);
            var sut2 = BankAgency.Transaction(Commercial, client2, client3, amount, TransactionType.International);            
            Assert.True(sut1);
            Assert.True(sut2);
            Assert.Equal(solde1 - (amount + 6), client1.Solde);
            Assert.Equal(solde2 -30, client2.Solde);
        }
    }
}
