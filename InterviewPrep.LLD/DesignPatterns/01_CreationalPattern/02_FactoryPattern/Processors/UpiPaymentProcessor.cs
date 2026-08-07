using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Processors
{
    public class UpiPaymentProcessor : IPaymentProcessor
    {
        public PaymentResponse ProcessPayment(PaymentRequests request)
        {
            return new PaymentResponse
            {
                IsSuccess = true,
                TransactionId = Guid.NewGuid().ToString(),
                Message = $"UPI payment of {request.Amount} processed successfully."
            };
        }
    }
}
