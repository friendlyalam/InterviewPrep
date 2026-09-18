using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._01_DecoratorPattern.Models
{
    public sealed class NotificationMessage
    {
        public string Recipient { get; init; } = string.Empty;

        public string Subject { get; init; } = string.Empty;

        public string Body { get; init; } = string.Empty;
    }
}

//Why a Model?

//Many tutorials do this:

//Send(string email,
//     string subject,
//     string body)

//Imagine after six months you need:

//Priority
//Attachment
//CC
//BCC
//CorrelationId
//TemplateId

//Now every method changes.

//Instead

//Send(NotificationMessage)

//is extensible.