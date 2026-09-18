using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services
{
    public sealed class PaymentService : IPaymentService
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing payment: {amount:C}");

            return true;
        }
    }
}
