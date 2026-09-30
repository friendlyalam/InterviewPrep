using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Models;

namespace InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces
{
    //    This is the orchestrator.
    //It doesn't perform hospital operations itself.
    //It coordinates specialized services.
    public class HospitalManagementService
    {
        private readonly IReceptionService _receptionService;
        private readonly IDoctorService _doctorService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IBillingService _billingService;

        public HospitalManagementService(
            IReceptionService receptionService,
            IDoctorService doctorService,
            IPharmacyService pharmacyService,
            IBillingService billingService)
        {
            _receptionService = receptionService;
            _doctorService = doctorService;
            _pharmacyService = pharmacyService;
            _billingService = billingService;
        }

        public void ProcessPatient(
            Patient patient,
            Appointment appointment)
        {
            _receptionService.RegisterPatient(patient);

            _receptionService.ScheduleAppointment(appointment);

            Prescription prescription =
                _doctorService.DiagnosePatient(patient);

            _pharmacyService.DispenseMedicine(prescription);

            Bill bill =
                _billingService.GenerateBill(patient);

            Console.WriteLine("--------------------------------");
            Console.WriteLine("Hospital Visit Completed");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Bill Amount : {bill.Amount}");
            Console.WriteLine();
        }
    }
}
