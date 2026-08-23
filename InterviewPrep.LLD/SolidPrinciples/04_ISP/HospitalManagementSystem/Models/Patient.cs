

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string Name { get; set; }= string.Empty;

        public int Age { get; set; }

        public string MobileNumber { get; set; }=string.Empty;
    }
}
