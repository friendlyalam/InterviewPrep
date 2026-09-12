using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._06_Task_Coordination
{
    public static class Task_Coordination
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("TASK COORDINATION");
            Console.WriteLine("========================================");

            await DemonstrateWhenAllAsync();

            Console.WriteLine();

            await DemonstrateWhenAllWithResultsAsync();

            Console.WriteLine();

            await DemonstrateWhenAnyAsync();

            Console.WriteLine();

            await DemonstrateWhenAnyWithResultsAsync();

            Console.WriteLine();

            await DemonstrateWhenAnyWithTimeoutAsync();

            Console.WriteLine();

            await DemonstrateWhenAnyWithCancellationAsync();

            Console.WriteLine();

            await DemonstrateWhenAllExceptionAsync();

            Console.WriteLine();

            await CompareSequentialAndWhenAllAsync();

            Console.WriteLine();

            Console.WriteLine("All Task Coordination demonstrations completed.");
        }

        private static async Task DemonstrateWhenAllAsync()
        {
            Console.WriteLine("1. TASK.WHENALL");
            Console.WriteLine("----------------------------------------");

            Task task1 = SimulateOperationAsync(
                "User API",
                2000);

            Task task2 = SimulateOperationAsync(
                "Order API",
                1500);

            Task task3 = SimulateOperationAsync(
                "Product API",
                1000);

            await Task.WhenAll(
                task1,
                task2,
                task3);

            Console.WriteLine(
                "All operations completed.");
        }

        private static async Task DemonstrateWhenAllWithResultsAsync()
        {
            Console.WriteLine("2. TASK.WHENALL WITH RESULTS");
            Console.WriteLine("----------------------------------------");

            Task<int> task1 =
                GetNumberAsync(10, 1000);

            Task<int> task2 =
                GetNumberAsync(20, 1500);

            Task<int> task3 =
                GetNumberAsync(30, 500);

            int[] results =
                await Task.WhenAll(
                    task1,
                    task2,
                    task3);

            Console.WriteLine("Results:");

            foreach (int result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine(
                "Result order follows the input task order.");
        }

        private static async Task DemonstrateWhenAnyAsync()
        {
            Console.WriteLine("3. TASK.WHENANY");
            Console.WriteLine("----------------------------------------");

            Task task1 = SimulateOperationAsync(
                "Server A",
                3000);

            Task task2 = SimulateOperationAsync(
                "Server B",
                1000);

            Task task3 = SimulateOperationAsync(
                "Server C",
                2000);

            Task completedTask =
                await Task.WhenAny(
                    task1,
                    task2,
                    task3);

            if (completedTask == task1)
            {
                Console.WriteLine(
                    "Server A completed first.");
            }
            else if (completedTask == task2)
            {
                Console.WriteLine(
                    "Server B completed first.");
            }
            else if (completedTask == task3)
            {
                Console.WriteLine(
                    "Server C completed first.");
            }

            // WhenAny does not cancel the remaining tasks.
            await Task.WhenAll(
                task1,
                task2,
                task3);

            Console.WriteLine(
                "All remaining operations eventually completed.");
        }

        private static async Task DemonstrateWhenAnyWithResultsAsync()
        {
            Console.WriteLine("4. TASK.WHENANY WITH RESULTS");
            Console.WriteLine("----------------------------------------");

            Task<string> task1 =
                GetServerResponseAsync(
                    "Server A",
                    2500);

            Task<string> task2 =
                GetServerResponseAsync(
                    "Server B",
                    1000);

            Task<string> task3 =
                GetServerResponseAsync(
                    "Server C",
                    2000);

            Task<string> completedTask =
                await Task.WhenAny(
                    task1,
                    task2,
                    task3);

            try
            {
                string result =
                    await completedTask;

                Console.WriteLine(
                    $"First completed result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"First completed task failed: {ex.Message}");
            }

            await Task.WhenAll(
                task1,
                task2,
                task3);
        }

        private static async Task DemonstrateWhenAnyWithTimeoutAsync()
        {
            Console.WriteLine("5. TASK.WHENANY - TIMEOUT");
            Console.WriteLine("----------------------------------------");

            Task operationTask =
                SimulateOperationAsync(
                    "Long Operation",
                    3000);

            Task timeoutTask =
                Task.Delay(1000);

            Task completedTask =
                await Task.WhenAny(
                    operationTask,
                    timeoutTask);

            if (completedTask == timeoutTask)
            {
                Console.WriteLine(
                    "Operation timed out.");
            }
            else
            {
                Console.WriteLine(
                    "Operation completed before timeout.");
            }

            // The timeout does not cancel the operation.
            await operationTask;
        }

        private static async Task DemonstrateWhenAnyWithCancellationAsync()
        {
            Console.WriteLine("6. TASK.WHENANY - CANCELLING REMAINING TASKS");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts =
                new();

            Task task1 =
                SimulateCancellableOperationAsync(
                    "Server A",
                    3000,
                    cts.Token);

            Task task2 =
                SimulateCancellableOperationAsync(
                    "Server B",
                    1000,
                    cts.Token);

            Task task3 =
                SimulateCancellableOperationAsync(
                    "Server C",
                    4000,
                    cts.Token);

            Task completedTask =
                await Task.WhenAny(
                    task1,
                    task2,
                    task3);

            Console.WriteLine(
                "First operation completed.");

            // WhenAny does not cancel automatically.
            // We explicitly request cancellation.
            cts.Cancel();

            try
            {
                await Task.WhenAll(
                    task1,
                    task2,
                    task3);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Remaining operations were cancelled.");
            }
        }

        private static async Task DemonstrateWhenAllExceptionAsync()
        {
            Console.WriteLine("7. TASK.WHENALL - EXCEPTION HANDLING");
            Console.WriteLine("----------------------------------------");

            Task task1 =
                SuccessfulOperationAsync(
                    "Task 1");

            Task task2 =
                FailingOperationAsync(
                    "Task 2");

            Task task3 =
                FailingOperationAsync(
                    "Task 3");

            Task allTasks =
                Task.WhenAll(
                    task1,
                    task2,
                    task3);

            try
            {
                await allTasks;
            }
            catch
            {
                Console.WriteLine(
                    "One or more tasks failed.");

                if (allTasks.Exception is not null)
                {
                    foreach (Exception exception
                        in allTasks.Exception.InnerExceptions)
                    {
                        Console.WriteLine(
                            $"Exception: {exception.Message}");
                    }
                }
            }
        }

        private static async Task CompareSequentialAndWhenAllAsync()
        {
            Console.WriteLine("8. SEQUENTIAL VS WHENALL");
            Console.WriteLine("----------------------------------------");

            Stopwatch sequentialStopwatch =
                Stopwatch.StartNew();

            await SimulateOperationAsync(
                "Sequential A",
                1000);

            await SimulateOperationAsync(
                "Sequential B",
                1500);

            await SimulateOperationAsync(
                "Sequential C",
                1000);

            sequentialStopwatch.Stop();

            Console.WriteLine(
                $"Sequential time: " +
                $"{sequentialStopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine();

            Stopwatch concurrentStopwatch =
                Stopwatch.StartNew();

            Task task1 =
                SimulateOperationAsync(
                    "Concurrent A",
                    1000);

            Task task2 =
                SimulateOperationAsync(
                    "Concurrent B",
                    1500);

            Task task3 =
                SimulateOperationAsync(
                    "Concurrent C",
                    1000);

            await Task.WhenAll(
                task1,
                task2,
                task3);

            concurrentStopwatch.Stop();

            Console.WriteLine(
                $"WhenAll time: " +
                $"{concurrentStopwatch.ElapsedMilliseconds} ms");
        }

        private static async Task SimulateOperationAsync(
            string operationName,
            int delayMilliseconds)
        {
            Console.WriteLine(
                $"{operationName} started | " +
                $"Thread: {Environment.CurrentManagedThreadId}");

            await Task.Delay(
                delayMilliseconds);

            Console.WriteLine(
                $"{operationName} completed | " +
                $"Thread: {Environment.CurrentManagedThreadId}");
        }

        private static async Task<int> GetNumberAsync(
            int number,
            int delayMilliseconds)
        {
            await Task.Delay(
                delayMilliseconds);

            return number;
        }

        private static async Task<string> GetServerResponseAsync(
            string serverName,
            int delayMilliseconds)
        {
            await Task.Delay(
                delayMilliseconds);

            return $"{serverName} response received.";
        }

        private static async Task SuccessfulOperationAsync(
            string operationName)
        {
            await Task.Delay(500);

            Console.WriteLine(
                $"{operationName} completed successfully.");
        }

        private static async Task FailingOperationAsync(
            string operationName)
        {
            await Task.Delay(700);

            throw new InvalidOperationException(
                $"{operationName} failed.");
        }

        private static async Task SimulateCancellableOperationAsync(
            string operationName,
            int delayMilliseconds,
            CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine(
                    $"{operationName} started.");

                await Task.Delay(
                    delayMilliseconds,
                    cancellationToken);

                Console.WriteLine(
                    $"{operationName} completed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    $"{operationName} cancelled.");

                throw;
            }
        }
    }
}

//call from Program.cs  
//await Task_Coordination.RunAsync();