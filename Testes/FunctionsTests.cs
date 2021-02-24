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
                 new Employee
                {
                    FirstName = "Ismail",
                    LastName = "Essabbagh",
                    CIN = "AE202368",
                    Role = Role.Chef,
                    Solde = 123456
                },
                 new Employee
                {
                    FirstName = "Aymen",
                    LastName = "Graoui",
                    CIN = "AE202888",
                    Role = Role.AdministrativeResponsible,
                    Solde = 4851
                },
                 new Employee
                {
                    FirstName = "Yassine",
                    LastName = "Essebbagh",
                    CIN = "AE481528",
                    Role = Role.ProductsResponsible,
                    Solde = 956
                },
                 new Employee
                {
                    FirstName = "Manal",
                    LastName = "Drissi",
                    CIN = "AE152638",
                    Role = Role.CommercialAgent,
                    Solde = 1253
                },
                 new Employee
                {
                    FirstName = "Omar",
                    LastName = "Essabbagh",
                    CIN = "AE887568",
                    Role = Role.CommercialAgent,
                    Solde = 1425
                },
                 new Employee
                {
                    FirstName = "Asmae",
                    LastName = "Alharaj",
                    CIN = "AE9865247",
                    Role = Role.CommercialAgent,
                    Solde = 6254
                }
            };
            NewAgency = new Agency(employees, 99999999);
            var clients = new List<Client>() {
                new Client{
                    FirstName="Said",
                    LastName="Mosker",
                    CIN="AX96325" ,
                    Solde=150
                },
                new Client{
                    FirstName="Samira",
                    LastName="Salamat",
                    CIN="AX97725"

                    ,Solde=130 },
                new Client
                {
                    FirstName = "Rania",
                    LastName = "Romaisae",
                    CIN = "AX098765",
                    Solde = 1850
                },
                new Client {
                    FirstName = "Rachid",
                    LastName = "Mohssin",
                    CIN = "AX96944",
                    Solde = 500 },
                new Client
                {
                    FirstName = "Kamal",
                    LastName = "Sadiki",
                    CIN = "AX98875",
                    Solde = 1750
                },
                new Client
                {
                    FirstName = "Said",
                    LastName = "Sadiki",
                    CIN = "AX02525",
                    Solde = 158000
                },
            };
            NewAgency.Clients = clients;
            var NewCreditOffer = new List<Offer>() {
                new Offer{Name="10000123",Amount=10000,DurationInMonth=12,InterestRate=3},
                new Offer{Name="qwqwe",Amount=50000,DurationInMonth=12,InterestRate=3},
                new Offer{Name="qeqrqwr",Amount=100000,DurationInMonth=12,InterestRate=3},
                new Offer{Name="qwdwdq",Amount=150000,DurationInMonth=12,InterestRate=3},
                new Offer{Name="dqwdqd",Amount=200000,DurationInMonth=12,InterestRate=3},
                new Offer{Name="qdcdxqd",Amount=10000,DurationInMonth=24,InterestRate=4},
                new Offer{Name="qrrcqrew",Amount=20000,DurationInMonth=24,InterestRate=4},
                new Offer{Name="xqreewq",Amount=50000,DurationInMonth=24,InterestRate=4},
                new Offer{Name="qercerqs",Amount=100000,DurationInMonth=24,InterestRate=4},
                new Offer{Name="qrewqs",Amount=150000,DurationInMonth=24,InterestRate=4},
                new Offer{Name="rqxewqd",Amount=200000,DurationInMonth=24,InterestRate=4}
            };
            NewAgency.CreditOffers = NewCreditOffer;
        }

        [Fact]
        public void RecruitSalesAgents()
        {
            var BankAgency = NewAgency;
            var NewEmploye = new List<Employee>() { new Employee { FirstName = "Zakaria", LastName = "Essabbagh", CIN = "AE948157", Role = Role.CommercialAgent, Solde = 625 } };
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
            var offer = new Offer { Name = "10000123", Amount = 100000, DurationInMonth = 12, MonthlyInstallment = 5000 };
            var sut = BankAgency.CreditAllocation(chef, client, offer);
            Assert.True(sut);
        }
        [Fact]
        public void PurchaseEquipment()
        {
            var BankAgency = NewAgency;
            var Admin = BankAgency.Employees.Find(emp => emp.Role == Role.AdministrativeResponsible);
            var amountOfPurchase = 5000;
            var caisse = BankAgency.Caisse;
            var sut1 = BankAgency.PurchaseEquipment(Admin, amountOfPurchase);
            var sut2 = caisse - amountOfPurchase;
            Assert.True(sut1);
            Assert.Equal(sut2, BankAgency.Caisse);
        }
        [Fact]
        public void SalaryTransfer()
        {
            var BankAgency = NewAgency;
            var Admin = BankAgency.Employees.Find(emp => emp.Role == Role.AdministrativeResponsible);
            var employe = BankAgency.Employees.Find(emp => emp.Role == Role.Chef);
            var salary = 20000;
            var newSolde = employe.Solde + salary;
            var newCaisse = BankAgency.Caisse - salary;
            var sut1 = BankAgency.SalaryTransfer(Admin, employe, salary);
            var sut2 = BankAgency.Employees.Find(emp => emp.Role == Role.Chef).Solde;
            Assert.True(sut1);
            Assert.Equal(newCaisse, BankAgency.Caisse);
            Assert.Equal(newSolde, sut2);
        }
        [Fact]
        public void WithdrawalAmount()
        {
            var BankAgency = NewAgency;
            var Admin = BankAgency.Employees.Find(emp => emp.Role == Role.AdministrativeResponsible);
            var client = BankAgency.Clients.Find(cl => cl.CIN == "AX02525");
            var amount = 6000;
            var solde = client.Solde - amount;
            var sut = BankAgency.WithdrawalAmount(Admin, client, amount);
            Assert.True(sut);
            Assert.Equal(solde, client.Solde);
        }
        [Fact]
        public void NewCreditOffer()
        {
            var BankAgency = NewAgency;
            var ProductResp = BankAgency.Employees.Find(emp => emp.Role == Role.ProductsResponsible);
            var offer1 = new Offer { Name = "sdsdccds", Amount = 500000, DurationInMonth = 12, InterestRate = 5 };
            var offer2 = new Offer { Name = "qeqrqwr", Amount = 100000, DurationInMonth = 12, InterestRate = 3 };
            var sut1 = BankAgency.NewCreditOffer(ProductResp, offer1);
            var sut2 = BankAgency.NewCreditOffer(ProductResp, offer2);
            Assert.True(sut1);
            Assert.False(sut2);
        }
        [Fact]
        public void CreateAccount()
        {
            var BankAgency = NewAgency;
            var ProductResp = BankAgency.Employees.Find(emp => emp.Role == Role.ProductsResponsible);
            var newClient = new Client { FirstName = "Taha", LastName = "Chakir", CIN = "AW845123", Solde = 1000 };
            var solde = newClient.Solde - 30;
            var sut1 = BankAgency.CreateClientAccount(ProductResp, newClient);
            var sut2 = BankAgency.Clients.Exists(cli => cli.CIN == "AW845123");
            var sut3 = BankAgency.Clients.Find(cli => cli.CIN == "AW845123").Solde;
            Assert.True(sut1);
            Assert.True(sut2);
            Assert.Equal(solde, sut3);
        }
        [Fact]
        public void Transfers()
        {
            var BankAgency = NewAgency;
            var Commercial = BankAgency.Employees.Find(emp => emp.Role == Role.CommercialAgent);
            var client = BankAgency.Clients.Find(cl => cl.CIN == "AX96944");
            var amount = 6580;
            var solde = client.Solde;
            var sut1 = BankAgency.Transfers(Commercial, client, amount);
            var sut2 = solde + amount;
            var sut3 = BankAgency.Clients.Find(cl => cl.CIN == "AX96944").Solde;
            Assert.True(sut1);
            Assert.Equal(sut2, sut3);

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
            var solde1 = client1.Solde-amount-6;
            var solde2 = client2.Solde - 30;
            var sut1 = BankAgency.Transaction(Commercial, client1, client2, amount, TransactionType.National);
            var sut2 = BankAgency.Transaction(Commercial, client2, client3, amount, TransactionType.International);
            Assert.True(sut1);
            Assert.True(sut2);
            Assert.Equal(solde1, client1.Solde);
            Assert.Equal(solde2, client2.Solde);
        }
    }
}
