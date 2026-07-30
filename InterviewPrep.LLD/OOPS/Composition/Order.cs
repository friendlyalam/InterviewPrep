
namespace InterviewPrep.LLD.OOPS.Composition
{
    public class Order
    {
        public int OrderId { get; }

        private readonly ShippingAddress _shippingAddress;

        public Order(
            int orderId,
            string street,
            string city,
            string country)
        {
            OrderId = orderId;

            _shippingAddress =
                new ShippingAddress(
                    street,
                    city,
                    country);
        }

        public void DisplayOrder()
        {
            Console.WriteLine($"Order Id : {OrderId}");
            Console.WriteLine();

            _shippingAddress.Display();
        }
    }
}

//Notice the important line:

//_shippingAddress =
//    new ShippingAddress(...);

//The parent creates the child.