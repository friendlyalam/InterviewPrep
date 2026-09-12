using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._04_Task
{
    public static class TaskDemo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("TASK DEMONSTRATION");
            Console.WriteLine("========================================");

            await BasicTaskAsync();

            Console.WriteLine();

            await TaskWithResultAsync();

            Console.WriteLine();

            await TaskWhenAllAsync();

            Console.WriteLine();

            await TaskWhenAnyAsync();

            Console.WriteLine();

            await TaskDelayAsync();

            Console.WriteLine();

            await TaskExceptionAsync();

            Console.WriteLine();

            await TaskCancellationAsync();

            Console.WriteLine();

            DemonstrateTaskCompletedAndFromResult();

            Console.WriteLine();

            await DemonstrateTaskRun();

            Console.WriteLine();

            Console.WriteLine("All Task demonstrations completed.");
        }

        // ------------------------------------------------------------
        // 1. Basic Task
        // ------------------------------------------------------------
        private static async Task BasicTaskAsync()
        {
            Console.WriteLine("1. BASIC TASK");
            Console.WriteLine("----------------------------------------");

            Task task = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task started | Thread: {Environment.CurrentManagedThreadId}");

                Thread.Sleep(1000);

                Console.WriteLine(
                    $"Task completed | Thread: {Environment.CurrentManagedThreadId}");
            });

            await task;

            Console.WriteLine("Basic task finished.");
        }

        // ------------------------------------------------------------
        // 2. Task with a return value
        // ------------------------------------------------------------
        private static async Task TaskWithResultAsync()
        {
            Console.WriteLine("2. TASK WITH RESULT");
            Console.WriteLine("----------------------------------------");

            Task<int> task = Task.Run(() =>
            {
                return 100;
            });

            int result = await task;

            Console.WriteLine($"Result: {result}");
        }

        // ------------------------------------------------------------
        // 3. Task.WhenAll
        // ------------------------------------------------------------
        private static async Task TaskWhenAllAsync()
        {
            Console.WriteLine("3. TASK.WHENALL");
            Console.WriteLine("----------------------------------------");

            Task task1 = SimulateOperationAsync("Operation A", 2000);
            Task task2 = SimulateOperationAsync("Operation B", 1500);
            Task task3 = SimulateOperationAsync("Operation C", 1000);

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine("All operations completed.");
        }

        // ------------------------------------------------------------
        // 4. Task.WhenAny
        // ------------------------------------------------------------
        private static async Task TaskWhenAnyAsync()
        {
            Console.WriteLine("4. TASK.WHENANY");
            Console.WriteLine("----------------------------------------");

            Task task1 = SimulateOperationAsync("Operation A", 3000);
            Task task2 = SimulateOperationAsync("Operation B", 1000);
            Task task3 = SimulateOperationAsync("Operation C", 2000);

            Task completedTask =
                await Task.WhenAny(task1, task2, task3);

            Console.WriteLine(
                $"First task completed: {completedTask.Id}");

            await Task.WhenAll(task1, task2, task3);
        }

        // ------------------------------------------------------------
        // 5. Task.Delay
        // ------------------------------------------------------------
        private static async Task TaskDelayAsync()
        {
            Console.WriteLine("5. TASK.DELAY");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Waiting asynchronously...");

            await Task.Delay(1000);

            Console.WriteLine("Async delay completed.");
        }

        // ------------------------------------------------------------
        // 6. Task exception handling
        // ------------------------------------------------------------
        private static async Task TaskExceptionAsync()
        {
            Console.WriteLine("6. TASK EXCEPTION");
            Console.WriteLine("----------------------------------------");

            try
            {
                await Task.Run(() =>
                {
                    throw new InvalidOperationException(
                        "Something went wrong inside the Task.");
                });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(
                    $"Exception handled: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // 7. Task cancellation
        // ------------------------------------------------------------
        private static async Task TaskCancellationAsync()
        {
            Console.WriteLine("7. TASK CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts =
                new CancellationTokenSource();

            Task task = Task.Run(async () =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    cts.Token.ThrowIfCancellationRequested();

                    Console.WriteLine($"Working... {i}");

                    await Task.Delay(300, cts.Token);
                }
            }, cts.Token);

            await Task.Delay(1000);

            Console.WriteLine("Requesting cancellation...");

            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was cancelled.");
            }
        }

        // ------------------------------------------------------------
        // 8. Task.CompletedTask and Task.FromResult
        // ------------------------------------------------------------
        private static void DemonstrateTaskCompletedAndFromResult()
        {
            Console.WriteLine("8. COMPLETED TASK AND FROMRESULT");
            Console.WriteLine("----------------------------------------");

            Task completedTask = Task.CompletedTask;

            Console.WriteLine(
                $"CompletedTask Status: {completedTask.Status}");

            Task<int> resultTask =
                Task.FromResult(500);

            Console.WriteLine(
                $"FromResult Value: {resultTask.Result}");
        }

        // ------------------------------------------------------------
        // 9. Task.Run for CPU-bound work
        // ------------------------------------------------------------
        private static async Task DemonstrateTaskRun()
        {
            Console.WriteLine("9. TASK.RUN - CPU-BOUND WORK");
            Console.WriteLine("----------------------------------------");

            Task task = Task.Run(() =>
            {
                Console.WriteLine(
                    $"CPU work started | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");

                PerformCpuWork();

                Console.WriteLine(
                    $"CPU work completed | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");
            });

            await task;
        }

        // ------------------------------------------------------------
        // Helper method
        // ------------------------------------------------------------
        private static async Task SimulateOperationAsync(
            string operationName,
            int delayMilliseconds)
        {
            Console.WriteLine(
                $"{operationName} started | " +
                $"Thread: {Environment.CurrentManagedThreadId}");

            await Task.Delay(delayMilliseconds);

            Console.WriteLine(
                $"{operationName} completed | " +
                $"Thread: {Environment.CurrentManagedThreadId}");
        }

        // ------------------------------------------------------------
        // CPU-bound work
        // ------------------------------------------------------------
        private static void PerformCpuWork()
        {
            double result = 0;

            for (int i = 0; i < 5_000_000; i++)
            {
                result += Math.Sqrt(i);
            }

            if (result < 0)
            {
                Console.WriteLine(result);
            }
        }
    }
}
