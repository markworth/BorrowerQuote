using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Quote.Core.Services;

namespace Quote.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private PaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            _paymentService = new PaymentService();
        }

        [Test, TestCaseSource(typeof(PaymentTestData), "LoanRates")]
        public decimal GivenAPRAmountAndLengthOfPayments_ThenCalculateMonthlyRepayment(decimal apr, decimal amount, int payments)
        {
            return Math.Round(_paymentService.CalculatePayments(apr, amount, payments), 2, MidpointRounding.AwayFromZero);
        }
    }

    public class PaymentTestData
    {
        public static IEnumerable LoanRates
        {
            get
            {
                yield return new TestCaseData(0.07M, 1000M, 36).Returns(30.78M);
                yield return new TestCaseData(0.034M, 7500M, 12).Returns(636.39M);
                yield return new TestCaseData(0.069M, 50000M, 36).Returns(1536.8M);
            }
        }
    }
}
