using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Services
{
    public class AuditService : IAuditService
    {
        public void WriteLog(Order order)
        {
            Console.WriteLine(
                $"Audit : Order {order.OrderId} processed.");
        }
    }
}

//Only audit logging.
