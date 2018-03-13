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
    public class QuoteServiceTests
    {
        [Test]
        public void GivenARequestToBorrow_WhenItCanBeAccepted_ThenCalculateTheQuote()
        {
            var lenders = new List<Lender>()
            {
                new Lender() {Name = "Bob", Rate = 0.075M, Available = 640},
                new Lender() {Name = "Jane", Rate = 0.069M, Available = 480},
                new Lender() {Name = "Fred", Rate = 0.071M, Available = 520},
                new Lender() {Name = "Angela", Rate = 0.071M, Available = 60}
            };

            var amountToBorrow = 1000;

            var quoteService = new QuoteService(new LenderService(), new PaymentService());
            var quote = quoteService.GetQuote(lenders, amountToBorrow);

            quote.Rate.ShouldBe(0.07M);
            quote.Amount.ShouldBe(1000);
            quote.MonthlyRepayment.ShouldBe(30.78M, 0.01M);
            quote.TotalRepayment.ShouldBe(1108.10M, 0.01M);
        }

        [Test]
        public void GivenARequestToBorrow_WhenItCantBeAccepted_ThenReturnNULL()
        {
            var lenders = new List<Lender>()
            {
                new Lender() {Name = "Bob", Rate = 0.075M, Available = 640},
                new Lender() {Name = "Jane", Rate = 0.069M, Available = 480},
                new Lender() {Name = "Fred", Rate = 0.071M, Available = 520},
                new Lender() {Name = "Angela", Rate = 0.071M, Available = 60}
            };

            var amountToBorrow = 15000;

            var quoteService = new QuoteService(new LenderService(), new PaymentService());
            var quote = quoteService.GetQuote(lenders, amountToBorrow);

            quote.ShouldBeNull();
        }
    }
}
