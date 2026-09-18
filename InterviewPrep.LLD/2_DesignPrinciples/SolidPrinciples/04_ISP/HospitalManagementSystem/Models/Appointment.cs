
namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }
    }
}
