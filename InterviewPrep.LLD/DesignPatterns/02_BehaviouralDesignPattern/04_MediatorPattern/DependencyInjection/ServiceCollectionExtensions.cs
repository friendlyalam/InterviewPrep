using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Handlers;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Requests;
using Microsoft.Extensions.DependencyInjection;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Mediator;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatorPattern(
            this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediators>();

            services.AddScoped<
                IRequestHandler<LeaveRequest, LeaveResult>,
                LeaveRequestHandler>();

            return services;
        }
    }
}
