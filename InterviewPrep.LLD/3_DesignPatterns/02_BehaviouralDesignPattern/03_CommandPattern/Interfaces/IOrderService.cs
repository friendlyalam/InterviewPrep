using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces
{
    public interface IOrderService
    {
        Task<CommandResult> CreateOrderAsync(
            int customerId,
            int productId,
            int quantity,
            decimal price,
            CancellationToken cancellationToken = default);

        Task<CommandResult> CancelOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);
    }
}

//Notice that the service doesn't know anything about:

//CreateOrderCommand
//CancelOrderCommand

//That's intentional.

//The service belongs to the business layer.

//The Command belongs to the request/application layer.