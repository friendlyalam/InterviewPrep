using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces
{
    public interface ICommand
    {
    }
}

//Why empty?

//Because different commands will contain different data.

//For example:

//CreateOrderCommand
//    CustomerId
//    ProductId
//    Quantity

//CancelOrderCommand
//    OrderId

//RefundOrderCommand
//    OrderId
//    Amount

//They all represent a command, but their data is different.