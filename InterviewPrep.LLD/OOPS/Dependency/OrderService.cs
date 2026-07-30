

namespace InterviewPrep.LLD.OOPS.Dependency
{
    public class OrderService
    {
        public void PlaceOrder(
            string customerName,
            string email,
            EmailService emailService)
        {
            Console.WriteLine(
                $"Order placed by {customerName}");

            emailService.SendEmail(email);
        }
    }
}

//Notice:

//EmailService is passed as a method parameter.

//This is dependency.