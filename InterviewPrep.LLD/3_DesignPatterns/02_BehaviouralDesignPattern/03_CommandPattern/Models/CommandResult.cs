using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models
{
    public sealed class CommandResult
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public object? Data { get; init; }

        public static CommandResult Succeeded(
            string message,
            object? data = null)
        {
            return new CommandResult
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static CommandResult Failed(
            string message)
        {
            return new CommandResult
            {
                Success = false,
                Message = message
            };
        }
    }
}

//Why CommandResult?

//Instead of:

//return true;

//we can return:

//Success
//Message
//Data

//For example:

//Success: true

//Message:
//"Order created successfully"

//Data:
//Order

//This is more useful for our demonstration.