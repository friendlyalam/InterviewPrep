using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces
{
    //    Notice this carefully.

    //These methods belong to one business capability.
    public interface IReceptionService
    {
        void RegisterPatient(Patient patient);

        void ScheduleAppointment(Appointment appointment);
    }
}

//This does not violate ISP.

//Both operations belong to Reception.