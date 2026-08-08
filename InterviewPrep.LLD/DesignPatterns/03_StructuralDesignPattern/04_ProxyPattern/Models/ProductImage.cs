

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._04_ProxyPattern.Models
{
    public sealed record ProductImage(
     int ProductId,
     string Url);
}

//    This is our simple response model.

//    Example:

//ProductId = 101
//    Url       = https://cdn.example.com/products/101.jpg


//    We're deliberately keeping the model small because the Proxy is the subject of the project, not image management.
