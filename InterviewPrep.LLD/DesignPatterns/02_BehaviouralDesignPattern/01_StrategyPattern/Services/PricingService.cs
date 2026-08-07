using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Stretigies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Services
{
    public sealed class PricingService : IPricingService
    {
        private readonly IEnumerable<IPricingStrategy> _pricingStrategies;

        public PricingService(IEnumerable<IPricingStrategy> pricingStrategies)
        {
            _pricingStrategies = pricingStrategies;
        }

        public decimal CalculatePrice(PricingContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var strategy = _pricingStrategies.FirstOrDefault(strategy =>
                strategy.StrategyName.Equals(
                    context.CustomerType,
                    StringComparison.OrdinalIgnoreCase));

            if (strategy is null)
            {
                throw new InvalidOperationException(
                    $"No pricing strategy found for '{context.CustomerType}'.");
            }

            return strategy.CalculatePrice(context);
        }
    }
}

//Let's Understand This
//Constructor
//private readonly IEnumerable<IPricingStrategy> _pricingStrategies;

//Suppose DI contains:

//RegularPricingStrategy

//FestivalPricingStrategy

//PremiumPricingStrategy

//.NET automatically injects

//IEnumerable<IPricingStrategy>

//which contains all registered implementations.

//No code changes are required when adding another strategy.

//Strategy Selection
//var strategy = _pricingStrategies.FirstOrDefault(...)

//This searches the collection.

//Example:

//Regular

//Festival

//Premium

//Customer:

//Festival

//Result:

//FestivalPricingStrategy
//Execution Flow
//PricingService

//↓

//Find Strategy

//↓

//Festival Strategy

//↓

//CalculatePrice()

//↓

//Return Price
//Why StrategyName?

//Remember we added:

//public string StrategyName => "Festival";

//Now we understand why.

//Without it we'd need:

//if(strategy is FestivalPricingStrategy)

//or

//strategy.GetType().Name

//Both are poor design.

//Instead each strategy identifies itself.
