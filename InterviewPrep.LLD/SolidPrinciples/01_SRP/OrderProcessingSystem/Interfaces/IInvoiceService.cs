

using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces
{
    public interface IInvoiceService
    {
        void GenerateInvoice(Order order);

    }
}
