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
            if (duration > 0 && amount >= 0 && interestRate >= 0)
            {
                Amount = amount;
                DurationInMonth = duration;
                InterestRate = interestRate;
                MonthlyInstallment = (amount * (1 + (interestRate / 100))) / duration;
            }
            else { Amount = -1; }
        }
    }
}
