using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Stretigies
{
    public sealed class RegularPricingStrategy : IPricingStrategy
    {
        public string StrategyName => "Regular";

        public decimal CalculatePrice(PricingContext context)
        {
            return context.Product.BasePrice;
        }
    }
}

//Business Logic

//No discount.

//₹1000

//↓

//₹1000