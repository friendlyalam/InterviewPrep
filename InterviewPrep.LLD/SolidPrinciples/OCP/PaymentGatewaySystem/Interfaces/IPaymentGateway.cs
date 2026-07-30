using InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Interfaces
{
    public interface IPaymentGateway
    {
        void ProcessPayment(PaymentRequest paymentRequest);
    }
}


//Why Interface?

//Tomorrow we may support

//Stripe
//Razorpay
//PayPal
//Amazon Pay
//Google Pay

//Every payment provider follows the same contract.