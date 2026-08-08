using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces
{
    public interface IRequest<TResult>
    {
}

//The request is generic because different requests can return different result types.
}
