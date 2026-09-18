using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Handlers
{
    public sealed class ManagerHandler : ExpenseHandler
    {
        private const decimal ApprovalLimit = 50_000m;

        public override string Handle(ExpenseRequest request)
        {
            if (request.Amount <= ApprovalLimit)
            {
                return $"Manager approved expense of ₹{request.Amount:N0}.";
            }

            return PassToNext(request);
        }
    }
}

//For:

//₹25,000

//the flow becomes:

//Team Lead
//   ↓
//Cannot approve
//   ↓
//Manager
//   ↓
//₹25,000 <= ₹50,000
//   ↓
//APPROVED