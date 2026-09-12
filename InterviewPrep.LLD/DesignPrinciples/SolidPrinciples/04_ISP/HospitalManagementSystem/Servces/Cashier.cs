using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces
{
    public class Cashier : IBillingService
    {
        public Bill GenerateBill(Patient patient)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Billing");
            Console.WriteLine("--------------------------------");

            Bill bill = new Bill
            {
                BillId = 501,
                Amount = 1500,
                IsPaid = false
            };

            Console.WriteLine($"Patient : {patient.Name}");
            Console.WriteLine($"Amount  : {bill.Amount}");

            Console.WriteLine("Bill generated successfully.\n");

            return bill;
        }
    }
}
