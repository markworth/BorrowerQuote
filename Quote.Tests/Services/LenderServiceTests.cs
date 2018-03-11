using System.Collections.Generic;
using NUnit.Framework;
using Quote.Core.Models;
using Quote.Core.Services;
using Shouldly;

namespace Quote.Tests.Services
{
    [TestFixture]
    public class LenderServiceTests
    {
        private LenderService _lenderService;

        [SetUp]
        public void Setup()
        {
            _lenderService = new LenderService();
        }

        [Test]
        public void Given3Lenders_WhenTheyHaveMoreThanEnoughToCoverLoan_ThenReturnTheCorrectAvailableAmount()
        {
            var lenders = new List<Lender>()
            {
                new Lender() { Name = "Second", Available = 260, Rate = 0.05M },
                new Lender() { Name = "NotToBeUsed", Available = 5000, Rate = 0.1M },
                new Lender() { Name = "Best", Available = 750, Rate = 0.01M }
            };

            var chosenLenders = _lenderService.CalculateRequiredLenders(lenders, 1000);

            chosenLenders.Count.ShouldBe(2);
            chosenLenders[0].Name.ShouldBe("Best");
            chosenLenders[0].Available.ShouldBe(750);
            chosenLenders[1].Name.ShouldBe("Second");
            chosenLenders[1].Available.ShouldBe(250);
        }

        [Test]
        public void Given3LendersOneWithNoAvailability_WhenTheyHaveMoreThanEnoughToCoverLoan_ThenReturnTheCorrectAvailableAmount()
        {
            var lenders = new List<Lender>()
            {
                new Lender() { Name = "Second", Available = 0, Rate = 0.05M },
                new Lender() { Name = "Third", Available = 5000, Rate = 0.1M },
                new Lender() { Name = "Best", Available = 750, Rate = 0.01M }
            };

            var chosenLenders = _lenderService.CalculateRequiredLenders(lenders, 1000);

            chosenLenders.Count.ShouldBe(2);
            chosenLenders[0].Name.ShouldBe("Best");
            chosenLenders[0].Available.ShouldBe(750);
            chosenLenders[1].Name.ShouldBe("Third");
            chosenLenders[1].Available.ShouldBe(250);
        }

        [Test]
        public void Given3LendersOne_WhenTheyDoNotHaveEnoughToCoverLoan_ThenReturnNULL()
        {
            var lenders = new List<Lender>()
            {
                new Lender() { Name = "Second", Available = 10, Rate = 0.05M },
                new Lender() { Name = "Third", Available = 50, Rate = 0.1M },
                new Lender() { Name = "Best", Available = 750, Rate = 0.01M }
            };

            var chosenLenders = _lenderService.CalculateRequiredLenders(lenders, 1000);
            chosenLenders.ShouldBeNull();
        }
    }
}
