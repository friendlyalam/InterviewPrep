using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces
{
    public interface IBillingService
    {
        Bill GenerateBill(Patient patient);
    }
}
