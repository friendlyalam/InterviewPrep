using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductImageServices(
            this IServiceCollection services)
        {
            services.AddScoped<ProductImageService>();

            services.AddScoped<IProductImageService>(serviceProvider =>
            {
                ProductImageService realService =
                    serviceProvider.GetRequiredService<ProductImageService>();

                return new ProductImageProxy(realService);
            });

            return services;
        }
    }
}
