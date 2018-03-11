using System;
using System.Collections.Generic;
using System.Text;

namespace Quote.Core.Models
{
    public class BorrowerQuote
    {
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal MonthlyRepayment { get; set; }
        public decimal TotalRepayment { get; set; }
    }
}
