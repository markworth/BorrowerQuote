using System;
using System.Collections.Generic;
using System.Text;
using Quote.Core.Models;

namespace Quote.Core.Services
{
    public interface IPaymentService
    {
        decimal CalculatePayments(decimal apr, decimal amount, int payments = 36);
        decimal CalculateRate(decimal amount, decimal amountCharged, int payments = 36);
    }

    public class PaymentService : IPaymentService
    {
        public decimal CalculatePayments(decimal apr, decimal amount, int payments = 36)
        {
            double c = Math.Pow(1 + (double) apr, 1.0 / 12) - 1;
            return amount * (decimal) (c / (1 - Math.Pow(1 + c, -payments)));
        }

        public decimal CalculateRate(decimal amount, decimal amountCharged, int payments = 36)
        {
            return 7; // chosen by fair dice roll
                      // guarenteed to be random.
        }
    }
}
