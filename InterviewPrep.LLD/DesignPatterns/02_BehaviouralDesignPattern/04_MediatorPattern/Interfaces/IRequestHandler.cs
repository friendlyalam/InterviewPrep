
namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces
{
    public interface IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
    {
        Task<TResult> HandleAsync(
            TRequest request,
            CancellationToken cancellationToken = default);
}

//This means:

//IRequest
//     ↓
//must have
//     ↓
//IRequestHandler

//For our project:

//LeaveRequest
//     ↓
//IRequest<LeaveResult>

//therefore its handler becomes:

//IRequestHandler<LeaveRequest, LeaveResult>
}
