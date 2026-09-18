using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExpenseApproval(
            this IServiceCollection services)
        {
            services.AddScoped<TeamLeadHandler>();
            services.AddScoped<ManagerHandler>();
            services.AddScoped<DirectorHandler>();

            services.AddScoped<ExpenseHandler>(serviceProvider =>
            {
                TeamLeadHandler teamLead =
                    serviceProvider.GetRequiredService<TeamLeadHandler>();

                ManagerHandler manager =
                    serviceProvider.GetRequiredService<ManagerHandler>();

                DirectorHandler director =
                    serviceProvider.GetRequiredService<DirectorHandler>();

                teamLead
                    .SetNext(manager)
                    .SetNext(director);

                return teamLead;
            });

            return services;
        }
    }
}
