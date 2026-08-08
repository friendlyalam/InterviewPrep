using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using InterviewPrep.LLD.SolidPrinciples.SRP.OrderProcessingSystem.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Repository
{
    public sealed class CommandOrderRepository : ICommandOrderRepository
    {
        private readonly ConcurrentDictionary<Guid, CommandOrder> _orders = new();

        public Task<CommandOrder?> GetByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _orders.TryGetValue(orderId, out CommandOrder? order);

            return Task.FromResult(order);
        }

        public Task AddAsync(
            CommandOrder order,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _orders[order.Id] = order;

            return Task.CompletedTask;
        }

        public Task UpdateAsync(
            CommandOrder order,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _orders[order.Id] = order;

            return Task.CompletedTask;
        }
    }
}

//Why ConcurrentDictionary?

//Our application may eventually process commands concurrently.

//For this demonstration:

//ConcurrentDictionary

//gives us a thread-safe in-memory store without introducing database configuration.