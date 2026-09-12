using InterviewPrep.CSharp.MultiThreading._05_Async_Await;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._05_Async_Await
{
    public static class Async_AwaitDemo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("ASYNC / AWAIT DEMONSTRATION");
            Console.WriteLine("========================================");

            await BasicAsyncAwaitAsync();

            Console.WriteLine();

            await TaskWithResultAsync();

            Console.WriteLine();

            await TaskDelayAsync();

            Console.WriteLine();

            await SequentialAsyncOperationsAsync();

            Console.WriteLine();

            await ConcurrentAsyncOperationsAsync();

            Console.WriteLine();

            await AsyncExceptionHandlingAsync();

            Console.WriteLine();

            await CancellationAsync();

            Console.WriteLine();

            await AsyncStreamAsync();

            Console.WriteLine();

            await DemonstrateCpuBoundWorkAsync();

            Console.WriteLine();

            Console.WriteLine("All Async/Await demonstrations completed.");
        }

        // ------------------------------------------------------------
        // 1. Basic async/await
        // ------------------------------------------------------------
        private static async Task BasicAsyncAwaitAsync()
        {
            Console.WriteLine("1. BASIC ASYNC/AWAIT");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine(
                $"Before await | Thread: {Environment.CurrentManagedThreadId}");

            await Task.Delay(1000);

            Console.WriteLine(
                $"After await  | Thread: {Environment.CurrentManagedThreadId}");
        }

        // ------------------------------------------------------------
        // 2. Async method returning a result
        // ------------------------------------------------------------
        private static async Task TaskWithResultAsync()
        {
            Console.WriteLine("2. TASK<T> RESULT");
            Console.WriteLine("----------------------------------------");

            int result = await GetNumberAsync();

            Console.WriteLine($"Result: {result}");
        }

        private static async Task<int> GetNumberAsync()
        {
            await Task.Delay(500);

            return 100;
        }

        // ------------------------------------------------------------
        // 3. Task.Delay vs Thread.Sleep
        // ------------------------------------------------------------
        private static async Task TaskDelayAsync()
        {
            Console.WriteLine("3. TASK.DELAY");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Starting asynchronous delay...");

            await Task.Delay(1000);

            Console.WriteLine("Asynchronous delay completed.");
        }

        // ------------------------------------------------------------
        // 4. Sequential async operations
        // ------------------------------------------------------------
        private static async Task SequentialAsyncOperationsAsync()
        {
            Console.WriteLine("4. SEQUENTIAL ASYNC OPERATIONS");
            Console.WriteLine("----------------------------------------");

            await SimulateApiCallAsync("User API", 1000);

            await SimulateApiCallAsync("Order API", 1000);

            await SimulateApiCallAsync("Product API", 1000);

            Console.WriteLine("Sequential operations completed.");
        }

        // ------------------------------------------------------------
        // 5. Concurrent async operations using WhenAll
        // ------------------------------------------------------------
        private static async Task ConcurrentAsyncOperationsAsync()
        {
            Console.WriteLine("5. CONCURRENT ASYNC OPERATIONS");
            Console.WriteLine("----------------------------------------");

            Task userTask =
                SimulateApiCallAsync("User API", 1000);

            Task orderTask =
                SimulateApiCallAsync("Order API", 1500);

            Task productTask =
                SimulateApiCallAsync("Product API", 800);

            await Task.WhenAll(
                userTask,
                orderTask,
                productTask);

            Console.WriteLine("All concurrent operations completed.");
        }

        // ------------------------------------------------------------
        // 6. Exception handling
        // ------------------------------------------------------------
        private static async Task AsyncExceptionHandlingAsync()
        {
            Console.WriteLine("6. ASYNC EXCEPTION HANDLING");
            Console.WriteLine("----------------------------------------");

            try
            {
                await OperationThatFailsAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(
                    $"Exception handled: {ex.Message}");
            }
        }

        private static async Task OperationThatFailsAsync()
        {
            await Task.Delay(300);

            throw new InvalidOperationException(
                "Something went wrong in the async operation.");
        }

        // ------------------------------------------------------------
        // 7. CancellationToken with async/await
        // ------------------------------------------------------------
        private static async Task CancellationAsync()
        {
            Console.WriteLine("7. ASYNC CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts =
                new CancellationTokenSource();

            Task task = ProcessWithCancellationAsync(cts.Token);

            await Task.Delay(1000);

            Console.WriteLine("Requesting cancellation...");

            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled.");
            }
        }

        private static async Task ProcessWithCancellationAsync(
            CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Console.WriteLine($"Processing item {i}");

                await Task.Delay(
                    300,
                    cancellationToken);
            }
        }

        // ------------------------------------------------------------
        // 8. IAsyncEnumerable / await foreach
        // ------------------------------------------------------------
        private static async Task AsyncStreamAsync()
        {
            Console.WriteLine("8. ASYNC STREAM");
            Console.WriteLine("----------------------------------------");

            await foreach (int number in GenerateNumbersAsync())
            {
                Console.WriteLine($"Received: {number}");
            }
        }

        private static async IAsyncEnumerable<int> GenerateNumbersAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(300);

                yield return i;
            }
        }

        // ------------------------------------------------------------
        // 9. CPU-bound work with Task.Run
        // ------------------------------------------------------------
        private static async Task DemonstrateCpuBoundWorkAsync()
        {
            Console.WriteLine("9. CPU-BOUND WORK WITH TASK.RUN");
            Console.WriteLine("----------------------------------------");

            int result = await Task.Run(() =>
            {
                Console.WriteLine(
                    $"CPU work started | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");

                return Calculate();
            });

            Console.WriteLine($"Calculation result: {result}");
        }

        private static int Calculate()
        {
            int result = 0;

            for (int i = 1; i <= 1_000_000; i++)
            {
                result += i % 10;
            }

            return result;
        }

        // ------------------------------------------------------------
        // Helper method - simulates asynchronous I/O
        // ------------------------------------------------------------
        private static async Task SimulateApiCallAsync(
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
    }
}
//Call from Program.cs:

//await Async_AwaitDemo.RunAsync();