using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Handlers
{
    public sealed class DirectorHandler : ExpenseHandler
    {
        private const decimal ApprovalLimit = 100_000m;

        public override string Handle(ExpenseRequest request)
        {
            if (request.Amount <= ApprovalLimit)
            {
                return $"Director approved expense of ₹{request.Amount:N0}.";
            }

            return "Expense rejected because it exceeds the approval limit.";
        }
    }
}
