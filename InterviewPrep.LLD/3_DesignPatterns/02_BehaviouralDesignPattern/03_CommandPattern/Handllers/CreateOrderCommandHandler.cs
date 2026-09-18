using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Commands;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Handllers
{
    public sealed class CreateOrderCommandHandler
    : ICommandHandler<CreateOrderCommand, CommandResult>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CommandResult> HandleAsync(
            CreateOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            return await _orderService.CreateOrderAsync(
                command.CustomerId,
                command.ProductId,
                command.Quantity,
                command.Price,
                cancellationToken);
        }
    }
}

//Notice the important separation:

//CreateOrderCommand
//        ↓
//contains data

//CreateOrderCommandHandler
//        ↓
//executes request

//OrderService
//        ↓
//contains business logic