using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Services
{
    public sealed class ProductImageService : IProductImageService
    {
        public async Task<ProductImage> GetImageAsync(int productId)
        {
            Console.WriteLine(
                $"[REAL SERVICE] Fetching image for product {productId}...");

            // Simulate an expensive external operation.
            await Task.Delay(1000);

            return new ProductImage(
                productId,
                $"https://cdn.example.com/products/{productId}.jpg");
        }
    }
}