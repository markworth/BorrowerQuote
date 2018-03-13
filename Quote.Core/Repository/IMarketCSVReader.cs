using System.Collections.Generic;
using Quote.Core.Models;

namespace Quote.Core.Repository
{
    public interface IMarketCSVReader
    {
        IList<Lender> GetLenders(string[] fileLines);
    }
}