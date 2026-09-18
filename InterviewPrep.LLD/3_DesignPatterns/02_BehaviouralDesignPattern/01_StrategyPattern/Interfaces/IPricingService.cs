using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces
{
    public interface IPricingService
    {
        decimal CalculatePrice(PricingContext context);
    }
}

//Why a PricingService?

//A common beginner implementation is:

//Program

//↓

//Strategy

//But enterprise applications usually introduce a service layer:

//Program

//↓

//PricingService

//↓

//Strategy

//This allows the service to:

//Resolve the correct strategy
//Log requests
//Validate input
//Handle errors
//Orchestrate business rules

//The strategies remain focused only on pricing algorithms.