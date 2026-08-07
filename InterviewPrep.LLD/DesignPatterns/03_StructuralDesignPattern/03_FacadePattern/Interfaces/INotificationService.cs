using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._03_FacadePattern.Interfaces
{
    public interface INotificationService
    {
        void SendConfirmation(string email);
    }
}
