using System;
using System.Collections.Generic;
using System.Text;
using Quote.Core.Models;

namespace Quote.Core.Services
{
    public class LoanFormatterService
    {
        public string FormatBorrowerQuote(BorrowerQuote quote)
        {
            return $@"Requested amount: £{quote.Amount}
Rate: {quote.Rate*100:0.0}%
Monthly repayment: £{quote.MonthlyRepayment:0.00}
Total repayment: £{quote.TotalRepayment:0.00}";
        }
    }
}
