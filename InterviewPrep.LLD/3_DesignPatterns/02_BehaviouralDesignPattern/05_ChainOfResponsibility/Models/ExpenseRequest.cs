using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._05_ChainOfResponsibility.Models
{
    public sealed record ExpenseRequest(
    int EmployeeId,
    decimal Amount,
    string Description);

//    This represents the request moving through our chain.

//    Example:

//var request = new ExpenseRequest(
//        EmployeeId: 101,
//        Amount: 25000,
//        Description: "Business travel");
}
