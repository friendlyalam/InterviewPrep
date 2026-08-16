using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces
{
    public class Pharmacist : IPharmacyService
    {
        public void DispenseMedicine(Prescription prescription)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Pharmacy");
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Diagnosis : {prescription.Diagnosis}");
            Console.WriteLine($"Medicines : {prescription.Medicines}");

            Console.WriteLine("Medicines dispensed successfully.\n");
        }
    }
}
