using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Models;
using InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._02_BehaviouralDesignPattern._04_MediatorPattern.Handlers
{
    public sealed class LeaveRequestHandler
    : IRequestHandler<LeaveRequest, LeaveResult>
    {
        public Task<LeaveResult> HandleAsync(
            LeaveRequest request,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (request.EmployeeId <= 0)
            {
                return Task.FromResult(
                    new LeaveResult(
                        false,
                        "Employee ID must be greater than zero."));
            }

            if (request.NumberOfDays <= 0)
            {
                return Task.FromResult(
                    new LeaveResult(
                        false,
                        "Leave days must be greater than zero."));
            }

            if (string.IsNullOrWhiteSpace(request.Reason))
            {
                return Task.FromResult(
                    new LeaveResult(
                        false,
                        "Leave reason is required."));
            }

            if (request.NumberOfDays > 10)
            {
                return Task.FromResult(
                    new LeaveResult(
                        false,
                        "Leave request cannot exceed 10 days."));
            }

            return Task.FromResult(
                new LeaveResult(
                    true,
                    $"Leave approved for employee {request.EmployeeId}."));
        }
    }
}
