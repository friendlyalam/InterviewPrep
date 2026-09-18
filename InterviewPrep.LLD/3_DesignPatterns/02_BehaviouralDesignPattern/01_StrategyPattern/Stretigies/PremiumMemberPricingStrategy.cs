using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Stretigies
{
    public sealed class PremiumMemberPricingStrategy : IPricingStrategy
    {
        public string StrategyName => "Premium";

        public decimal CalculatePrice(PricingContext context)
        {
            decimal premiumDiscount = 15;

            decimal discount =
                context.Product.BasePrice *
                premiumDiscount / 100;

            return context.Product.BasePrice - discount;
        }
    }
}
