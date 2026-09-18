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
    public sealed class RefundOrderCommandHandler
    : ICommandHandler<RefundOrderCommand, CommandResult>
    {
        private readonly IPaymentService _paymentService;

        public RefundOrderCommandHandler(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<CommandResult> HandleAsync(
            RefundOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            return await _paymentService.RefundAsync(
                command.OrderId,
                command.Amount,
                cancellationToken);
        }
    }
}
