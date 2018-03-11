using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Quote.Core.Models;
using Quote.Core.Services;
using Shouldly;

namespace Quote.Tests.Services
{
    [TestFixture]
    public class LoanFormatterServiceTests
    {
        private LoanFormatterService _loanFormatterService;

        [SetUp]
        public void Setup()
        {
            _loanFormatterService = new LoanFormatterService();
        }

        [Test]
        public void GivenAQuote_WhenItIsValid_ThenFormatAResponse()
        {
            var quote = new BorrowerQuote()
            {
                Amount = 1000,
                MonthlyRepayment = 30.78111111M,
                TotalRepayment = 1108.10101010101M,
                Rate = 7.01M
            };

            var output = _loanFormatterService.FormatBorrowerQuote(quote);

            output.ShouldContain("Requested amount: £1000");
            output.ShouldContain("Rate: 7.0%");
            output.ShouldContain("Monthly repayment: £30.78");
            output.ShouldContain("Total repayment: £1108.10");
        }
    }
}
