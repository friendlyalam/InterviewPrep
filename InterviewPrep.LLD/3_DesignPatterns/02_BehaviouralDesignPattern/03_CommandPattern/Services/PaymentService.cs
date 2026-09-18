using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Services
{
    public sealed class PaymentService : IPaymentService
    {
        public async Task<CommandResult> RefundAsync(
            Guid orderId,
            decimal amount,
            CancellationToken cancellationToken = default)
        {
            if (orderId == Guid.Empty)
            {
                return CommandResult.Failed(
                    "Order ID is required.");
            }

            if (amount <= 0)
            {
                return CommandResult.Failed(
                    "Refund amount must be greater than zero.");
            }

            await Task.Delay(100, cancellationToken);

            return CommandResult.Succeeded(
                $"Refund of {amount:C} processed successfully.");
        }
    }
}

//The Task.Delay simply simulates an external payment API call.

//In a real application:

//PaymentService
//      ↓
//HTTP Client
//      ↓
//Payment Provider