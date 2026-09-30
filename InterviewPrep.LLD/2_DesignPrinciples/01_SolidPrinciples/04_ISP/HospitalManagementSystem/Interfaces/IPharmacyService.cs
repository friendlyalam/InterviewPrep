using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces
{
    public interface IPharmacyService
    {
        void DispenseMedicine(Prescription prescription);
    }
}
