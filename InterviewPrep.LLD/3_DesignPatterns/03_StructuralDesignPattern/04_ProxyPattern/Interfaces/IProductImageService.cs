using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Interfaces
{
    public interface IProductImageService
    {
        Task<ProductImage> GetImageAsync(int productId);
    }
}

//Now both the real service and the proxy will implement the same interface.

