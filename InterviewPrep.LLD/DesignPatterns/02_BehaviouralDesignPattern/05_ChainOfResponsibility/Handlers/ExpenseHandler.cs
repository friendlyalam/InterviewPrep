using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Handlers
{
    public abstract class ExpenseHandler
    {
        protected ExpenseHandler? Next { get; private set; }

        public ExpenseHandler SetNext(ExpenseHandler next)
        {
            Next = next;
            return next;
        }

        public abstract string Handle(ExpenseRequest request);

        protected string PassToNext(ExpenseRequest request)
        {
            if (Next is null)
            {
                return "Expense request could not be approved.";
            }

            return Next.Handle(request);
        }
    }
}
