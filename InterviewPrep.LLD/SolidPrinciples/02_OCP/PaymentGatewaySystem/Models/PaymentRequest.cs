
namespace InterviewPrep.LLD.SolidPrinciples.OCP.PaymentGatewaySystem.Models
{
    public class PaymentRequest
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; }= string.Empty;
    }
}
//Responsibility

//Represents payment information.

//Nothing else.