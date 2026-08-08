using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces
{

    public interface IPaymentService
    {
        Task<CommandResult> RefundAsync(
            Guid orderId,
            decimal amount,
            CancellationToken cancellationToken = default);
    }
}

//Later:

//IPaymentService
//       ↓
//PaymentService
//       ↓
//Stripe / Razorpay / Internal Payment API

//For our interview project, we'll simulate the payment gateway.