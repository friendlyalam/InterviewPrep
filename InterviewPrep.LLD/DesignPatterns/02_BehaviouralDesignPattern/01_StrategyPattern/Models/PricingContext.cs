
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;
using InterviewPrep.LLD.SolidPrinciples.ISP.HospitalManagementSystem.Servces;
using System.Diagnostics.Metrics;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models
{
    public sealed class PricingContext
    {
        public Product Product { get; init; } = default!;

        public string CustomerType { get; init; } = string.Empty;

        public bool IsFestivalSale { get; init; }

        public decimal DiscountPercentage { get; init; }
    }
}

//Why PricingContext?

//Instead of this:

//CalculatePrice(product,
//               customerType,
//               isFestival,
//               discount,
//               country,
//               membership,
//               ...)

//we use:

//CalculatePrice(pricingContext)

//This is cleaner, easier to maintain, and allows us to extend the context later without changing every strategy method signature.