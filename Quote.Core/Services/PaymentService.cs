using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quote.Core.Models;

namespace Quote.Core.Services
{
    public interface IPaymentService
    {
        decimal CalculatePayments(decimal apr, decimal amount, int payments = 36);
        decimal CalculateRate(decimal amount, decimal amountCharged, decimal lowest, decimal highest, int payments = 36);
    }

    public class PaymentService : IPaymentService
    {
        public decimal CalculatePayments(decimal apr, decimal amount, int payments = 36)
        {
            double c = Math.Pow(1 + (double) apr, 1.0 / 12) - 1;
            return amount * (decimal) (c / (1 - Math.Pow(1 + c, -payments)));
        }

        public decimal CalculateRate(decimal amount, decimal monthlyPayment, decimal lowest, decimal highest, int payments = 36)
        {
            // Here's my thought process:
            // You can't rearrange the calculate payments formula as the APR is in multiple places
            // so the best thing you can do is to use numerical methods to calculate the answer.

            // But rather than break out Newton-Raphson or Bisection I thought that the simplest thing to do
            // would just be to iterate through all the values at the required precision and find the best fit.
            // (as the answer will be between the lowest and highest lender APRs - which would be my starting left and right brackets in Bisection)

            decimal closest = lowest;
            decimal leastDifferenceFromAmountCharged = decimal.MaxValue;

            for (decimal aprTest = lowest; aprTest < highest; aprTest += 0.001M)
            {
                var difference = Math.Abs(CalculatePayments(aprTest, amount, payments) - monthlyPayment);
                if (difference < leastDifferenceFromAmountCharged)
                {
                    leastDifferenceFromAmountCharged = difference;
                    closest = aprTest;
                }
            }

            return closest;
        }
    }
}
