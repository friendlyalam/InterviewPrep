using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Services
{
    public class RazorpayPaymentGateway : IPaymentGateway
    {
        public void ProcessPayment(PaymentRequest paymentRequest)
        {
            Console.WriteLine("--------- RAZORPAY ---------");

            Console.WriteLine(
                $"Processing ₹{paymentRequest.Amount} using Razorpay.");

            Console.WriteLine("Razorpay Payment Successful");

            Console.WriteLine();
        }
    }
}
