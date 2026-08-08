using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Models
{
    public sealed record LeaveResult(
     bool Approved,
     string Message);
}
