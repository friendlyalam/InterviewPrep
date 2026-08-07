using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services
{
    public sealed class ShippingService : IShippingService
    {
        public string CreateShipment(string deliveryAddress)
        {
            Console.WriteLine($"Scheduling shipment to {deliveryAddress}");

            return Guid.NewGuid().ToString("N")[..10].ToUpper();
        }
    }
}
