

using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces
{
    public interface IAuditService
    {
        void WriteLog(Order order);

    }
}
