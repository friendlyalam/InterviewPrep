using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services
{
    public class InvoiceService : IInvoiceService
    {
        public void GenerateInvoice(Order order)
        {
            decimal total = order.Price * order.Quantity;

            Console.WriteLine(
                $"Invoice Generated : ₹{total}");
        }
    }
}

//Only invoice.
