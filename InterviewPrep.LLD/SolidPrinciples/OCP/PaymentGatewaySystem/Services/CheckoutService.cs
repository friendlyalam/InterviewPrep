

using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models;
using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Services;

namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Services
{
    //This is the most important class.
    public class CheckoutService
    {
        private readonly IPaymentGateway _paymentGateway;

        public CheckoutService(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        public void Checkout(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
                throw new ArgumentNullException(nameof(paymentRequest));

            if (paymentRequest.Amount <= 0)
                throw new Exception("Invalid payment amount.");

            Console.WriteLine("================================");

            Console.WriteLine("CHECKOUT STARTED");

            Console.WriteLine("================================");

            Console.WriteLine($"Order Id : {paymentRequest.OrderId}");

            Console.WriteLine($"Customer : {paymentRequest.CustomerName}");

            Console.WriteLine($"Amount : ₹{paymentRequest.Amount}");

            Console.WriteLine();

            _paymentGateway.ProcessPayment(paymentRequest);

            Console.WriteLine("Checkout Completed.");

            Console.WriteLine();
        }
    }
}

//Why does CheckoutService depend on IPaymentGateway?

//Instead of

//StripePaymentGateway stripe = new StripePaymentGateway();

//it depends on

//IPaymentGateway

//Therefore,

//CheckoutService doesn't care

//whether payment happens through

//Stripe

//or

//Razorpay

//or

//PayPal

//This is the key to OCP.
