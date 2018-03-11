using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Quote.Core.Models;

namespace Quote.Core.Repository
{
    public class CSVMarketRepository : IMarketRepository
    {
        public IList<Lender> GetLenders(string[] csvFileLines)
        {
            var lenders = new List<Lender>();

            foreach (var line in csvFileLines.Skip(1))
            {
                var tokens = line.Split(',');
                if (tokens.Length != 3)
                {
                    throw new CsvFileNotValidException();
                }

                try
                {
                    lenders.Add(new Lender() { Name = tokens[0], Rate = decimal.Parse(tokens[1]), Available = decimal.Parse(tokens[2])});
                }
                catch
                {
                    throw new CsvFileNotValidException();
                }
            }

            return lenders;
        }
    }
}
