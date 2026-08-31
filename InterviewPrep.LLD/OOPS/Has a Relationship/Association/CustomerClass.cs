
namespace InterviewPrep.LLD.OOPS.Association
{

    //    Notice carefully.
    //The Customer does not store the Order.
    //The Customer simply uses the Order.
    public class Customer
    {
        public string Name { get; }

        public Customer(string name)
        {
            Name = name;
        }

        public void PlaceOrder(Order order)
        {
            Console.WriteLine($"{Name} placed an order.");

            Console.WriteLine();

            order.DisplayOrder();
        }
    }
}
