using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services
{
    public sealed class InvoiceService : IInvoiceService
    {
        public string GenerateInvoice(int customerId)
        {
            Console.WriteLine("Generating invoice...");

            return $"INV-{customerId}-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}
