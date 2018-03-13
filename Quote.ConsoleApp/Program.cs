using System;
using System.IO;
using Quote.Core.Repository;
using Quote.Core.Services;

namespace Quote.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: quote.exe [csvfile] [amount]");
                return;
            }

            var csvFile = args[0];

            if (!File.Exists(csvFile))
            {
                Console.WriteLine($"{csvFile} does not exist");
                return;
            }

            var amountToBorrow = int.Parse(args[1]);

            var quoteService = new QuoteService(new LenderService(), new PaymentService());
            var csvMarketRepository = new MarketCSVReader();

            if (!quoteService.IsValidRequestedAmount(amountToBorrow))
            {
                Console.WriteLine("Loan amount must be between £1000 and £15000 in a £100 increment");
                return;
            }

            var quote = quoteService.GetQuote(csvMarketRepository.GetLenders(File.ReadAllLines(csvFile)), amountToBorrow);

            if (quote == null)
            {
                Console.WriteLine("It is not possible to provide a quote at this time");
                return;
            }

            var output = new LoanFormatterService().FormatBorrowerQuote(quote);

            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
