
namespace InterviewPrep.CSharp.MultiThreading._18_Advanced_Async
{
    public static class Advanced_Async_Demo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("ADVANCED ASYNC");
            Console.WriteLine("========================================");

            await DemonstrateIAsyncEnumerableAsync();

            Console.WriteLine();

            await DemonstrateIAsyncEnumerableCancellationAsync();

            Console.WriteLine();

            await DemonstrateValueTaskAsync();

            Console.WriteLine();

            await DemonstrateValueTaskAsTaskAsync();

            Console.WriteLine();

            await DemonstrateStreamingVsMaterializationAsync();

            Console.WriteLine();

            await DemonstrateConcurrentAsyncProcessingAsync();

            Console.WriteLine();

            Console.WriteLine("All Advanced Async demonstrations completed.");
        }

        // ============================================================
        // 1. IAsyncEnumerable<T>
        // ============================================================

        private static async Task DemonstrateIAsyncEnumerableAsync()
        {
            Console.WriteLine("1. IASYNCENUMERABLE<T>");
            Console.WriteLine("----------------------------------------");

            await foreach (int number in GenerateNumbersAsync())
            {
                Console.WriteLine($"Received: {number}");
            }

            Console.WriteLine(
                "Items were produced and consumed incrementally.");
        }

        private static async IAsyncEnumerable<int> GenerateNumbersAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(300);

                yield return i;
            }
        }

        // ============================================================
        // 2. IAsyncEnumerable<T> + CancellationToken
        // ============================================================

        private static async Task DemonstrateIAsyncEnumerableCancellationAsync()
        {
            Console.WriteLine("2. IASYNCENUMERABLE<T> + CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts =
                new();

            cts.CancelAfter(TimeSpan.FromSeconds(1.2));

            try
            {
                await foreach (int number in GenerateNumbersWithCancellationAsync()
                    .WithCancellation(cts.Token))
                {
                    Console.WriteLine($"Received: {number}");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Async enumeration was cancelled.");
            }
        }

        private static async IAsyncEnumerable<int> GenerateNumbersWithCancellationAsync(
            [System.Runtime.CompilerServices.EnumeratorCancellation]
        CancellationToken cancellationToken = default)
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(
                    300,
                    cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                yield return i;
            }
        }

        // ============================================================
        // 3. ValueTask<T> - synchronous completion
        // ============================================================

        private static async Task DemonstrateValueTaskAsync()
        {
            Console.WriteLine("3. VALUETASK<T>");
            Console.WriteLine("----------------------------------------");

            int value =
                await GetCachedValueAsync();

            Console.WriteLine($"Value: {value}");

            Console.WriteLine(
                "ValueTask can efficiently represent an already-completed result.");
        }

        private static ValueTask<int> GetCachedValueAsync()
        {
            // Simulates a frequently synchronously completed operation.
            return ValueTask.FromResult(100);
        }

        // ============================================================
        // 4. ValueTask<T> wrapping an actual asynchronous Task
        // ============================================================

        private static async Task DemonstrateValueTaskAsTaskAsync()
        {
            Console.WriteLine("4. VALUETASK<T> WRAPPING TASK<T>");
            Console.WriteLine("----------------------------------------");

            int value =
                await GetValueFromRemoteSourceAsync();

            Console.WriteLine($"Value: {value}");

            Console.WriteLine(
                "ValueTask can represent either an immediate result or an asynchronous Task.");
        }

        private static ValueTask<int> GetValueFromRemoteSourceAsync()
        {
            return new ValueTask<int>(
                LoadValueAsync());
        }

        private static async Task<int> LoadValueAsync()
        {
            await Task.Delay(500);

            return 200;
        }

        // ============================================================
        // 5. Streaming vs materialization
        // ============================================================

        private static async Task DemonstrateStreamingVsMaterializationAsync()
        {
            Console.WriteLine("5. STREAMING VS MATERIALIZATION");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Streaming:");

            await foreach (int number in GenerateLargeDataAsync())
            {
                Console.WriteLine($"Processed immediately: {number}");
            }

            Console.WriteLine();

            Console.WriteLine("Materialization:");

            List<int> numbers =
                await GetAllDataAsync();

            foreach (int number in numbers)
            {
                Console.WriteLine($"Processed after loading: {number}");
            }

            Console.WriteLine();

            Console.WriteLine(
                "Streaming allows processing items before the entire sequence is available.");
        }

        private static async IAsyncEnumerable<int> GenerateLargeDataAsync()
        {
            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(200);

                yield return i;
            }
        }

        private static async Task<List<int>> GetAllDataAsync()
        {
            List<int> numbers = new();

            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(200);

                numbers.Add(i);
            }

            return numbers;
        }

        // ============================================================
        // 6. Concurrent processing of independent async operations
        // ============================================================

        private static async Task DemonstrateConcurrentAsyncProcessingAsync()
        {
            Console.WriteLine("6. CONCURRENT ASYNC PROCESSING");
            Console.WriteLine("----------------------------------------");

            Task<string> productTask =
                GetProductAsync();

            Task<string> inventoryTask =
                GetInventoryAsync();

            Task<string> paymentTask =
                GetPaymentStatusAsync();

            await Task.WhenAll(
                productTask,
                inventoryTask,
                paymentTask);

            Console.WriteLine($"Product: {await productTask}");
            Console.WriteLine($"Inventory: {await inventoryTask}");
            Console.WriteLine($"Payment: {await paymentTask}");

            Console.WriteLine();

            Console.WriteLine(
                "Independent I/O-bound operations can make progress concurrently.");
        }

        private static async Task<string> GetProductAsync()
        {
            await Task.Delay(500);

            return "Product information received";
        }

        private static async Task<string> GetInventoryAsync()
        {
            await Task.Delay(700);

            return "Inventory information received";
        }

        private static async Task<string> GetPaymentStatusAsync()
        {
            await Task.Delay(400);

            return "Payment status received";
        }
    }
}
