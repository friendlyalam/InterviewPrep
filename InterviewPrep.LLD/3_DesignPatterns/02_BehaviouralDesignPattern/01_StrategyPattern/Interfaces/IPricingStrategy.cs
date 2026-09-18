using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;
using InterviewPrep.LLD.OOPS.Association;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces
{
    public interface IPricingStrategy
    {
        string StrategyName { get; }

        decimal CalculatePrice(PricingContext context);
    }
}

//Why StrategyName?

//Many tutorials don't include this.

//In enterprise applications, we need a way to identify strategies.

//Later, our PricingService will resolve the correct strategy by matching:

//Customer Type

//↓

//Strategy Name

//↓

//Strategy

//Without StrategyName, we'd end up writing type checks or large switch statements, which defeats the purpose.

//Why Return decimal?

//Prices require decimal precision.

//Never use:

//double

//or

//float

//for money.

//In .NET, always use:

//decimal