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

        [Test, TestCaseSource(typeof(PaymentTestData), "MonthlyPayments")]
        public decimal GivenAPRAmountAndLengthOfPayments_ThenCalculateMonthlyRepayment(decimal apr, decimal amount, int payments)
        {
            return Math.Round(_paymentService.CalculatePayments(apr, amount, payments), 2, MidpointRounding.AwayFromZero);
        }

        [Test, TestCaseSource(typeof(PaymentTestData), "APR")]
        public decimal GivenMonthlyPaymentAmountChargedAndLengthOfPayments_ThenCalculateAPR(decimal amount, decimal monthlyPayment, decimal lowest, decimal highest, int payments)
        {
            return _paymentService.CalculateRate(amount, monthlyPayment, lowest, highest, payments);
        }
    }

    public class PaymentTestData
    {
        public static IEnumerable MonthlyPayments
        {
            get
            {
                yield return new TestCaseData(0.07M, 1000M, 36).Returns(30.78M);
                yield return new TestCaseData(0.034M, 7500M, 12).Returns(636.39M);
                yield return new TestCaseData(0.069M, 50000M, 36).Returns(1536.8M);
            }
        }

        public static IEnumerable APR
        {
            get
            {
                yield return new TestCaseData(1000M, 30.78M, 0.069M, 0.071M, 36).Returns(0.07M);
                yield return new TestCaseData(7500M, 636.39M, 0.02M, 0.04M, 12).Returns(0.034M);
                yield return new TestCaseData(50000M, 1536.8M, 0.05M, 0.08M, 36).Returns(0.069M);
            }
        }
    }
}
