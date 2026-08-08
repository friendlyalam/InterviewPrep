using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces
{
    public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand
    {
        Task<TResult> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken = default);
    }
}


//This is an important enterprise abstraction.

//For example:

//CreateOrderCommand
//        ↓
//ICommandHandler<CreateOrderCommand, CommandResult>

//and:

//CancelOrderCommand
//        ↓
//ICommandHandler<CancelOrderCommand, CommandResult>

//The handler is responsible for executing the command.