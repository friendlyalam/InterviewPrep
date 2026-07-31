using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Processors
{
    public class NetBankingPaymentProcessor : IPaymentProcessor
    {
        public PaymentResponse ProcessPayment(PaymentRequests request)
        {
            return new PaymentResponse
            {
                IsSuccess = true,
                TransactionId = Guid.NewGuid().ToString(),
                Message = $"Net Banking payment of {request.Amount} processed successfully."
            };
        }
    }
}
