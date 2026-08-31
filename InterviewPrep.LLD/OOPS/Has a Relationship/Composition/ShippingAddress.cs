

namespace InterviewPrep.LLD.OOPS.Composition
{
    public class ShippingAddress
    {
        public string Street { get; }
        public string City { get; }
        public string Country { get; }

        public ShippingAddress(
            string street,
            string city,
            string country)
        {
            Street = street;
            City = city;
            Country = country;
        }

        public void Display()
        {
            Console.WriteLine($"Street : {Street}");
            Console.WriteLine($"City    : {City}");
            Console.WriteLine($"Country : {Country}");
        }
    }
}
