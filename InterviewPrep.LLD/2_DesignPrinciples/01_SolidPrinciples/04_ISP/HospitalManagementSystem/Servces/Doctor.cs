

using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces
{
    public class Doctor : IDoctorService
    {
        public Prescription DiagnosePatient(Patient patient)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Doctor Consultation");
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Examining patient : {patient.Name}");

            Prescription prescription = new Prescription
            {
                PrescriptionId = 1001,
                Diagnosis = "Viral Fever",
                Medicines = "Paracetamol, Vitamin C"
            };

            Console.WriteLine("Diagnosis completed.\n");

            return prescription;
        }
    }
}
