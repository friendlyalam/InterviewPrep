using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Factories;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Services
{
    public class CheckoutServices
    {
        public PaymentResponse Checkout(PaymentRequests requests)
        {
            var paymentProcessor =
                PaymentProcessorFactory.Create(requests.PaymentMethod);

            return paymentProcessor.ProcessPayment(requests);
        }
    }
}

//Notice that CheckoutService has no idea whether it's using Credit Card, UPI, Wallet, or Net Banking.