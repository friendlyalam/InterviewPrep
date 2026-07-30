using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Services
{
    public class PayPalPaymentGateway : IPaymentGateway
    {
        public void ProcessPayment(PaymentRequest paymentRequest)
        {
            Console.WriteLine("---------- PAYPAL ----------");

            Console.WriteLine(
                $"Processing ₹{paymentRequest.Amount} using PayPal.");

            Console.WriteLine("PayPal Payment Successful");

            Console.WriteLine();
        }
    }
}
