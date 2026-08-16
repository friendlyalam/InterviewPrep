
namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models
{
    public class Bill
    {
        public int BillId { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
    }
}
