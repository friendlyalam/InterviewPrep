using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Services
{
    public sealed class ProductImageProxy : IProductImageService
    {
        private readonly IProductImageService _realService;

        private readonly Dictionary<int, ProductImage> _cache = new();

        public ProductImageProxy(IProductImageService realService)
        {
            _realService = realService;
        }

        public async Task<ProductImage> GetImageAsync(int productId)
        {
            if (_cache.TryGetValue(productId, out ProductImage? cachedImage))
            {
                Console.WriteLine(
                    $"[PROXY] Cache hit for product {productId}.");

                return cachedImage;
            }

            Console.WriteLine(
                $"[PROXY] Cache miss for product {productId}.");

            ProductImage image =
                await _realService.GetImageAsync(productId);

            _cache[productId] = image;

            Console.WriteLine(
                $"[PROXY] Image cached for product {productId}.");

            return image;
        }
    }
}
