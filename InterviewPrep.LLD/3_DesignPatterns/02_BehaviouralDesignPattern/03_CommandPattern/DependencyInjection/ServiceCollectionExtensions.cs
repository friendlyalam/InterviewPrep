using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Commands;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Handllers;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Services;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandPatternServices(
            this IServiceCollection services)
        {
            // Repository
            services.AddSingleton<IOrderRepository, OrderRepository>();

            // Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            // Command Handlers
            services.AddScoped<
                ICommandHandler<CreateOrderCommand, CommandResult>,
                CreateOrderCommandHandler>();

            services.AddScoped<
                ICommandHandler<CancelOrderCommand, CommandResult>,
                CancelOrderCommandHandler>();

            services.AddScoped<
                ICommandHandler<RefundOrderCommand, CommandResult>,
                RefundOrderCommandHandler>();

            return services;
        }
    }
}
