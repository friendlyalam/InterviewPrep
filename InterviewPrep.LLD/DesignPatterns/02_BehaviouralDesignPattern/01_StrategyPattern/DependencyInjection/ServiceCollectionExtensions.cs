using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Services;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Stretigies;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPricingServices(
            this IServiceCollection services)
        {
            services.AddTransient<IPricingStrategy, RegularPricingStrategy>();

            services.AddTransient<IPricingStrategy, FestivalPricingStrategy>();

            services.AddTransient<IPricingStrategy, PremiumMemberPricingStrategy>();

            services.AddTransient<IPricingService, PricingService>();

            return services;
        }
    }
}

//Why Multiple Registrations?

//Many developers ask:

//services.AddTransient < IPricingStrategy, ...> ();

//three times?

//Yes.

//That's exactly how .NET DI works.

//Later

//IEnumerable<IPricingStrategy>

//contains

//Regular

//Festival

//Premium