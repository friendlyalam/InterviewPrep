
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces
{
    public interface IInventoryService
    {
        void UpdateStock(Order order);

    }
}
