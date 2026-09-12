
namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public string Medicines { get; set; }= string.Empty;
    }
}
