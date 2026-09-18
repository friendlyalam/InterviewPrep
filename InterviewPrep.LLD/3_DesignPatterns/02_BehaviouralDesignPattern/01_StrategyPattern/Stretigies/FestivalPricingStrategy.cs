using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Stretigies
{
    public sealed class FestivalPricingStrategy : IPricingStrategy
    {
        public string StrategyName => "Festival";

        public decimal CalculatePrice(PricingContext context)
        {
            decimal discount =
                context.Product.BasePrice *
                context.DiscountPercentage / 100;

            return context.Product.BasePrice - discount;
        }
    }
}


//Business Logic

//Suppose

//Price = ₹1000

//Festival Discount = 20%

//Calculation

//1000

//↓

//200 Discount

//↓

//800