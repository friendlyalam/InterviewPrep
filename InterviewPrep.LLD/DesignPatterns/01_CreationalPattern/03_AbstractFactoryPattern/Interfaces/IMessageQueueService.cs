using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces
{
    public interface IMessageQueueService
    {
        CloudProvider Provider { get; }

        void Publish(string message);

        string Receive();
    }
}
