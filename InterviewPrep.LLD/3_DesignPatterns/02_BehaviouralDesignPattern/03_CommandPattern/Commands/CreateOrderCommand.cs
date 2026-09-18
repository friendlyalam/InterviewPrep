using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Commands
{
    public sealed record CreateOrderCommand(
    int CustomerId,
    int ProductId,
    int Quantity,
    decimal Price) : ICommand;

    }
//    This is the most important file in this step.
