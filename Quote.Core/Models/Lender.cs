using System;
using System.Collections.Generic;
using System.Text;

namespace Quote.Core.Models
{
    public class Lender
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public decimal Available { get; set; }
    }
}
