using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces
{
    public interface IMediator
    {
        Task<TResult> SendAsync<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken = default);
    }

  //  This is the central abstraction our application will use.
}
