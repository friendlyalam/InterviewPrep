
namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
