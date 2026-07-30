
namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
