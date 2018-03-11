using System.Collections.Generic;
using Quote.Core.Models;

namespace Quote.Core.Repository
{
    public interface IMarketRepository
    {
        IList<Lender> GetLenders(string[] fileLines);
    }
}