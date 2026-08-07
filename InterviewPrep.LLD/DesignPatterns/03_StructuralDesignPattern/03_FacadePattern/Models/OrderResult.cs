using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Models
{
    public sealed class OrderResult
    {
        public bool Success { get; init; }

        public string OrderNumber { get; init; } = string.Empty;

        public string Message { get; init; } = string.Empty;
    }
}
