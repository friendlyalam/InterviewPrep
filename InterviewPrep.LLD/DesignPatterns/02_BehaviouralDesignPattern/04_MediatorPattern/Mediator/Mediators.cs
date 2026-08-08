using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Mediator
{
    public sealed class Mediators : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediators(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken = default)
        {
            Type requestType = request.GetType();

            Type handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(requestType, typeof(TResult));

            object? handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
            {
                throw new InvalidOperationException(
                    $"No handler registered for request type '{requestType.Name}'.");
            }

            var handleMethod = handlerType.GetMethod(
                nameof(IRequestHandler<IRequest<TResult>, TResult>.HandleAsync));

            if (handleMethod is null)
            {
                throw new InvalidOperationException(
                    $"HandleAsync method not found for '{requestType.Name}'.");
            }

            object? result = handleMethod.Invoke(
                handler,
                new object?[]
                {
                request,
                cancellationToken
                });

            if (result is Task<TResult> task)
            {
                return await task;
            }

            throw new InvalidOperationException(
                $"Handler returned an invalid result for '{requestType.Name}'.");
        }
    }
}
