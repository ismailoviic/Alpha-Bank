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

        public Credit(decimal amount, int duration, decimal interestRate)
        {
            Amount = amount;
            DurationInMonth = duration;
            InterestRate = interestRate;
            MonthlyInstallment = (amount * (interestRate / 100)) / duration;
        }
    }
}
