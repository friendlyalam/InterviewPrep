using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces
{
    public interface IDoctorService
    {
        Prescription DiagnosePatient(Patient patient);
    }
}

//Again,

//Diagnosis belongs to Doctor.

//One responsibility.