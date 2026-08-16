

using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces
{

    //    The receptionist only performs reception-related work.
    //It does not know how to diagnose patients, dispense medicines, or generate bills.
    public class Receptionist : IReceptionService
    {
        public void RegisterPatient(Patient patient)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Patient Registration");
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Patient Id   : {patient.PatientId}");
            Console.WriteLine($"Name         : {patient.Name}");
            Console.WriteLine($"Age          : {patient.Age}");
            Console.WriteLine($"Mobile       : {patient.MobileNumber}");

            Console.WriteLine("Patient registered successfully.\n");
        }

        public void ScheduleAppointment(Appointment appointment)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Appointment Scheduling");
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Appointment Id : {appointment.AppointmentId}");
            Console.WriteLine($"Doctor         : {appointment.DoctorName}");
            Console.WriteLine($"Date           : {appointment.AppointmentDate}");

            Console.WriteLine("Appointment scheduled successfully.\n");
        }
    }
}
