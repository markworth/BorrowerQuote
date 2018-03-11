using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quote.Core.Models;

namespace Quote.Core.Services
{
    public interface ILenderService
    {
        IList<Lender> CalculateRequiredLenders(IList<Lender> lenders, decimal amountNeeded);
    }

    public class LenderService : ILenderService
    {
        public IList<Lender> CalculateRequiredLenders(IList<Lender> lenders, decimal amountNeeded)
        {
            var chosenLenders = new List<Lender>();
            var amountLeft = amountNeeded;

            foreach (var lender in lenders.Where(x => x.Available > 0).OrderBy(x => x.Rate))
            {
                var amount = Math.Min(lender.Available, amountLeft);
                chosenLenders.Add(new Lender() { Name = lender.Name, Available = amount, Rate = lender.Rate });
                amountLeft -= amount;

                if (amountLeft == 0) break;
            }

            return amountLeft == 0 ? chosenLenders : null;
        }
    }
}
