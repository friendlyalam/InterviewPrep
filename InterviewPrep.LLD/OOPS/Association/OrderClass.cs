
namespace InterviewPrep.LLD.OOPS.Association
{
    public class Order
    {
        public int OrderId { get; }
        public string ItemName { get; }

        public Order(int orderId, string itemName)
        {
            OrderId = orderId;
            ItemName = itemName;
        }

        public void DisplayOrder()
        {
            Console.WriteLine($"Order Id : {OrderId}");
            Console.WriteLine($"Item      : {ItemName}");
        }
    }
}