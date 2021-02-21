using Alpha_Bank.Implementation;
using Alpha_Bank.Testes;
using System;
using Xunit;

namespace Alpha_Bank
{
    public class ConstructorsTests
    {
        [Fact]
        public void PersonContructor()
        {
            var firstName = "Ismail";
            var lastName = "Essabbagh";
            var cin = "AE202368";
            var sut1 = new Person(firstName, lastName, cin);
            var sut2 = new Person(firstName, cin, lastName);
            Assert.Equal(firstName, sut1.FirstName);
            Assert.Equal(lastName, sut1.LastName);
            Assert.Equal(cin, sut1.CIN);
            Assert.NotEqual(firstName, sut2.FirstName);
            Assert.True(string.IsNullOrEmpty(sut2.LastName));

        }
        [Fact]
        public void ClientTest()
        {
            var firstName = "Ismail";
            var lastName = "Essabbagh";
            var cin = "AE202368";
            var solde = 500;
            var sut1 = new Person(firstName, lastName, cin);
            var sut2 = new Client(firstName, lastName, cin, solde);
            var sut3 = new Client(sut1, solde);

            Assert.Equal(firstName, sut2.FirstName);
            Assert.Equal(lastName, sut2.LastName);
            Assert.Equal(cin, sut2.CIN);
            Assert.Equal(solde, sut2.Solde);

            Assert.True(sut2.ComparePersonTo(sut3));
        }

        [Fact]
        public void CreditTest()
        {
            var Amount = 100;
            var DurationInMonth = 12;
            var InterestRate = 5;
            var MonthlyInstallment = 8.75m;
            var sut1 = new Credit(Amount, DurationInMonth, InterestRate);
            var sut2 = new Credit(Amount, 0, InterestRate);
            Assert.Equal(MonthlyInstallment, sut1.MonthlyInstallment);
            Assert.Equal(-1, sut2.Amount);
        }
        [Fact]
        public void EmployeeTest()
        {

            var firstName = "Ismail";
            var lastName = "Essabbagh";
            var cin = "AE202368";
            var solde = 500;
            var role = Role.Chef;
            var sut1 = new Person(firstName, lastName, cin);
            var sut2 = new Employee(sut1, role, solde);

            Assert.Equal(firstName, sut2.FirstName);
            Assert.Equal(lastName, sut2.LastName);
            Assert.Equal(cin, sut2.CIN);
            Assert.Equal(solde, sut2.Solde);
            Assert.True(sut2.ComparePersonTo(sut1));
        }
        [Fact]
        public void FunctionTestConstructorTest()
        {
            var sut = new FunctionsTests();
            Assert.Equal(6, sut.NewAgency.Employees.Count);
            Assert.Equal(99999999, sut.NewAgency.Caisse);
        }

    }
}
