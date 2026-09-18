

namespace InterviewPrep.LLD.OOPS.Dependency
{
    public class EmailService
    {
        public void SendEmail(string customerEmail)
        {
            Console.WriteLine(
                $"Confirmation email sent to {customerEmail}");
        }
    }
}