using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quote.Core.Models;

namespace Quote.Core.Services
{
    public class QuoteService
    {
        private ILenderService _lenderService;
        private IPaymentService _paymentService;

        public QuoteService(ILenderService lenderService, IPaymentService paymentService)
        {
            _lenderService = lenderService;
            _paymentService = paymentService;
        }

        public BorrowerQuote GetQuote(IList<Lender> lenders, decimal amountToBorrow)
        {
            var lendersToUse = _lenderService.CalculateRequiredLenders(lenders, amountToBorrow);
            var lenderPayments = lendersToUse.Select(x => _paymentService.CalculatePayments(x.Rate, x.Available)).ToList();

            var totalMonthlyPayments = lenderPayments.Sum();
            var totalPayment = totalMonthlyPayments * 36;

            var financeCharge = totalPayment - amountToBorrow;

            var rate = _paymentService.CalculateRate(amountToBorrow, financeCharge);

            return new BorrowerQuote()
            {
                Rate = rate,
                Amount = amountToBorrow,
                MonthlyRepayment = totalMonthlyPayments,
                TotalRepayment = totalPayment
            };
        }
    }
}
