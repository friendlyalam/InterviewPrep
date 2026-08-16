
namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models
{
    public class PaymentRequest
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
//Responsibility

//Represents payment information.

//Nothing else.