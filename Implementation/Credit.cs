using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha_Bank.Implementation
{
    public class Credit
    {
        public decimal Amount = 0;
        public int DurationInMonth { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyInstallment { get; set; }
    }

    public class Offer : Credit
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
