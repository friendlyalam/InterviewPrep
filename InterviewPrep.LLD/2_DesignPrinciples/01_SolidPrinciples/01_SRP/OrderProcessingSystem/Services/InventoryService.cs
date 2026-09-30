using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services
{
    public class InventoryService : IInventoryService
    {
        public void UpdateStock(Order order)
        {
            Console.WriteLine(
                $"{order.Quantity} item(s) deducted from inventory.");
        }
    }
}
//Only inventory.