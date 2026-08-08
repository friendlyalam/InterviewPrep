using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Services
{
    public sealed class OrderService : IOrderService
    {
        private readonly ICommandOrderRepository _orderRepository;

        public OrderService(ICommandOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CommandResult> CreateOrderAsync(
            int customerId,
            int productId,
            int quantity,
            decimal price,
            CancellationToken cancellationToken = default)
        {
            if (customerId <= 0)
            {
                return CommandResult.Failed(
                    "Customer ID must be greater than zero.");
            }

            if (productId <= 0)
            {
                return CommandResult.Failed(
                    "Product ID must be greater than zero.");
            }

            if (quantity <= 0)
            {
                return CommandResult.Failed(
                    "Quantity must be greater than zero.");
            }

            if (price <= 0)
            {
                return CommandResult.Failed(
                    "Price must be greater than zero.");
            }

            CommandOrder order = new()
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
                TotalAmount = quantity * price,
                Status = "Created",
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(
                order,
                cancellationToken);

            return CommandResult.Succeeded(
                "Order created successfully.",
                order);
        }

        public async Task<CommandResult> CancelOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            if (orderId == Guid.Empty)
            {
                return CommandResult.Failed(
                    "Order ID is required.");
            }

            CommandOrder? order = await _orderRepository.GetByIdAsync(
                orderId,
                cancellationToken);

            if (order is null)
            {
                return CommandResult.Failed(
                    "Order was not found.");
            }

            if (order.Status == "Cancelled")
            {
                return CommandResult.Failed(
                    "Order is already cancelled.");
            }

            order.Status = "Cancelled";

            await _orderRepository.UpdateAsync(
                order,
                cancellationToken);

            return CommandResult.Succeeded(
                "Order cancelled successfully.",
                order);
        }
    }
}
