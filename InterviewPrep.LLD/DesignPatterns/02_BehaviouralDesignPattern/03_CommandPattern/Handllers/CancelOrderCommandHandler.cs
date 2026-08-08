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
    public sealed class CancelOrderCommandHandler
     : ICommandHandler<CancelOrderCommand, CommandResult>
    {
        private readonly IOrderService _orderService;

        public CancelOrderCommandHandler(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CommandResult> HandleAsync(
            CancelOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            return await _orderService.CancelOrderAsync(
                command.OrderId,
                cancellationToken);
        }
    }
}
