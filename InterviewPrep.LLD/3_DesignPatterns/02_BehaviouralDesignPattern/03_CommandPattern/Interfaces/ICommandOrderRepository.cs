using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._03_CommandPattern.Interfaces
{
    public interface ICommandOrderRepository
    {
            Task<CommandOrder?> GetByIdAsync(
                Guid orderId,
                CancellationToken cancellationToken = default);

            Task AddAsync(
                CommandOrder order,
                CancellationToken cancellationToken = default);

            Task UpdateAsync(
                CommandOrder order,
                CancellationToken cancellationToken = default);
        }
    }
