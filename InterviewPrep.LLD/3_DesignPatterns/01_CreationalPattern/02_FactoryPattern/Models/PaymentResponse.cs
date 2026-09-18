
namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._02_FactoryPattern.Models
{
    public class PaymentResponse
    {
        public bool IsSuccess { get; set; }

        public string TransactionId { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
