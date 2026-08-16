using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine(
                $"Order {order.OrderId} saved into database.");
        }
    }
}

//Responsibility

//Only database operations.

//Nothing else.