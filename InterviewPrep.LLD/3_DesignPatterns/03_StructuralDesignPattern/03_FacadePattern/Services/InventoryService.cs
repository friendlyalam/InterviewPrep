using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Services
{
    public sealed class InventoryService : IInventoryService
    {
        public bool IsAvailable(int productId, int quantity)
        {
            Console.WriteLine("Checking inventory...");

            // Simulate inventory lookup

            return quantity <= 10;
        }
    }
}
