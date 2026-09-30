using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Services
{
    public class StripePaymentGateway : IPaymentGateway
    {
        public void ProcessPayment(PaymentRequest paymentRequest)
        {
            Console.WriteLine("---------- STRIPE ----------");

            Console.WriteLine(
                $"Processing ₹{paymentRequest.Amount} using Stripe.");

            Console.WriteLine("Stripe Payment Successful");

            Console.WriteLine();
        }
    }
}