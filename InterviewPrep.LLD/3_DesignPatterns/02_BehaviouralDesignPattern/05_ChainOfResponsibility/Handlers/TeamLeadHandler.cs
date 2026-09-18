using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Handlers
{
    public sealed class TeamLeadHandler : ExpenseHandler
    {
        private const decimal ApprovalLimit = 10_000m;

        public override string Handle(ExpenseRequest request)
        {
            if (request.Amount <= ApprovalLimit)
            {
                return $"Team Lead approved expense of ₹{request.Amount:N0}.";
            }

            return PassToNext(request);
        }
    }
}

//What happens?

//If:

//Amount = ₹8,000

//then:

//TeamLead
//   ↓
//₹8,000 <= ₹10,000
//   ↓
//APPROVE

//But if:

//Amount = ₹20,000

//then:

//TeamLead
//   ↓
//Cannot approve
//   ↓
//PassToNext()