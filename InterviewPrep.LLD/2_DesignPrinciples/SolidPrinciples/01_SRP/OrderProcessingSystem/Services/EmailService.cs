using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services
{
    public class EmailService : IEmailService
    {
        public void SendConfirmation(Order order)
        {
            Console.WriteLine(
                $"Confirmation email sent to {order.CustomerEmail}");
        }
    }
}

//Only email.