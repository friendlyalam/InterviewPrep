

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._01_StrategyPattern.Models
{
    public sealed class Product
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public decimal BasePrice { get; init; }
    }
}

//Why so few properties?

//Because this project demonstrates the Strategy Pattern, not a complete e-commerce system.

//Adding Category, SKU, Brand, Description, Inventory, Weight, etc., would only distract from the pattern.