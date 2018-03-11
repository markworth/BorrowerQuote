using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Quote.Core;
using Quote.Core.Repository;
using Shouldly;

namespace Quote.Tests.Repository
{
    [TestFixture]
    public class CSVMarketRepositoryTests
    {
        private CSVMarketRepository _csvMarketRepository;

        [SetUp]
        public void Setup()
        {
            _csvMarketRepository = new CSVMarketRepository();
        }

        [Test]
        public void GivenACSVList_WhenItIsValid_ThenReturnLenders()
        {
            var inputLines = @"Lender,Rate,Available
Bob,0.075,640
Jane,0.069,480
Fred,0.071,520
Mary,0.104,170
John,0.081,320
Dave,0.074,140
Angela,0.071,60".Split('\n');

            var lenders = _csvMarketRepository.GetLenders(inputLines);

            lenders.Count.ShouldBe(7);
            lenders[0].Name.ShouldBe("Bob");
            lenders[0].Rate.ShouldBe(0.075M);
            lenders[0].Available.ShouldBe(640M);

            // TODO: Add lenders 1-5

            lenders[6].Name.ShouldBe("Angela");
            lenders[6].Rate.ShouldBe(0.071M);
            lenders[6].Available.ShouldBe(60M);
        }

        [Test]
        public void GivenACSVList_WhenItDoesNotHaveEnoughColumns_ThenThrowException()
        {
            var inputLines = @"Lender,Rate,Available
Bob,0.075
Angela,0.071,60".Split('\n');

            bool hasThrown = false;
            try
            {
                var lenders = _csvMarketRepository.GetLenders(inputLines);
            }
            catch (CsvFileNotValidException)
            {
                hasThrown = true;
            }

            hasThrown.ShouldBeTrue();
        }

        [Test]
        public void GivenACSVList_WhenItHasAnInvalidNumber_ThenThrowException()
        {
            var inputLines = @"Lender,Rate,Available
Bob,CHEESE!,640
Angela,0.071,60".Split('\n');

            bool hasThrown = false;
            try
            {
                var lenders = _csvMarketRepository.GetLenders(inputLines);
            }
            catch (CsvFileNotValidException)
            {
                hasThrown = true;
            }

            hasThrown.ShouldBeTrue();
        }

    }
}
