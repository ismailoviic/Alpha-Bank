using System;
using Xunit;

namespace AlphaBankImplementation
{
    public class ConstructorsTests
    {
        [Fact]
        public void PersonContructor()
        {
            var firstName = "Ismail";
            var lastName = "Essabbagh";
            var cin = "AE202368";
            var sut1 = new Person { FirstName = firstName, LastName = lastName, CIN = cin };
            Assert.Equal(firstName, sut1.FirstName);
            Assert.Equal(lastName, sut1.LastName);
            Assert.Equal(cin, sut1.CIN);

        }
        [Fact]
        public void ClientTest()
        {
            var firstName = "Ismail";
            var lastName = "Essabbagh";
            var cin = "AE202368";
            var solde = 500;
            var sut = new Client { FirstName = firstName, LastName = lastName, CIN = cin, Solde = solde };

            Assert.Equal(firstName, sut.FirstName);
            Assert.Equal(lastName, sut.LastName);
            Assert.Equal(cin, sut.CIN);
            Assert.Equal(solde, sut.Solde);
        }

        [Fact]
        public void CreditTest()
        {
            var Amount = 100;
            var DurationInMonth = 12;
            var InterestRate = 5;
            var MonthlyInstallment = 8.75m;
            var sut = new Credit { Amount = Amount, DurationInMonth = DurationInMonth, MonthlyInstallment = MonthlyInstallment, InterestRate = InterestRate };
            Assert.Equal(Amount, sut.Amount);
            Assert.Equal(DurationInMonth, sut.DurationInMonth);
            Assert.Equal(InterestRate, sut.InterestRate);
            Assert.Equal(MonthlyInstallment, sut.MonthlyInstallment);
        }
        [Fact]
        public void EmployeeTest()
        {

            var firstName = "Ismail";
            var lastName = "Essabbagh";
            var cin = "AE202368";
            var solde = 500;
            var role = Role.Chef;
            var sut = new Employee { FirstName = firstName, LastName = lastName, CIN = cin, Role = role, Solde = solde };

            Assert.Equal(firstName, sut.FirstName);
            Assert.Equal(lastName, sut.LastName);
            Assert.Equal(cin, sut.CIN);
            Assert.Equal(solde, sut.Solde);
            Assert.Equal(role, sut.Role);
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
