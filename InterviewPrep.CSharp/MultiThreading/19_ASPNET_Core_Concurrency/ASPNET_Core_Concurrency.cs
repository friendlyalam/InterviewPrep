using System.Collections.Concurrent;

namespace InterviewPrep.CSharp.MultiThreading._19_ASPNET_Core_Concurrency
{
    public static class ASPNET_Core_Concurrency_Demo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("ASP.NET CORE CONCURRENCY");
            Console.WriteLine("========================================");

            await DemonstrateAsyncRequestPatternAsync();

            Console.WriteLine();

            await DemonstrateConcurrentIndependentOperationsAsync();

            Console.WriteLine();

            await DemonstrateControlledConcurrencyAsync();

            Console.WriteLine();

            await DemonstrateThreadSafeSharedStateAsync();

            Console.WriteLine();

            await DemonstrateConcurrentDictionaryAsync();

            Console.WriteLine();

            await DemonstrateCancellationAsync();

            Console.WriteLine();

            await DemonstrateRequestScopedStateAsync();

            Console.WriteLine();

            await DemonstrateProducerConsumerAsync();

            Console.WriteLine();

            await DemonstrateDatabaseConcurrencyConceptAsync();

            Console.WriteLine();

            Console.WriteLine("All ASP.NET Core concurrency demonstrations completed.");
        }

        // ============================================================
        // 1. Async request pattern
        // ============================================================

        private static async Task DemonstrateAsyncRequestPatternAsync()
        {
            Console.WriteLine("1. ASYNC REQUEST PATTERN");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Request started.");

            string result =
                await SimulateDatabaseCallAsync();

            Console.WriteLine($"Database result: {result}");
            Console.WriteLine("Request completed.");

            Console.WriteLine(
                "Async I/O avoids unnecessarily blocking a ThreadPool thread.");
        }

        private static async Task<string> SimulateDatabaseCallAsync()
        {
            await Task.Delay(500);

            return "Order data loaded";
        }

        // ============================================================
        // 2. Concurrent independent operations
        // ============================================================

        private static async Task DemonstrateConcurrentIndependentOperationsAsync()
        {
            Console.WriteLine("2. CONCURRENT INDEPENDENT OPERATIONS");
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

            Console.WriteLine(
                $"Product: {await productTask}");

            Console.WriteLine(
                $"Inventory: {await inventoryTask}");

            Console.WriteLine(
                $"Payment: {await paymentTask}");

            Console.WriteLine(
                "Independent I/O operations were started concurrently.");
        }

        private static async Task<string> GetProductAsync()
        {
            await Task.Delay(500);

            return "Product loaded";
        }

        private static async Task<string> GetInventoryAsync()
        {
            await Task.Delay(700);

            return "Inventory loaded";
        }

        private static async Task<string> GetPaymentStatusAsync()
        {
            await Task.Delay(400);

            return "Payment status loaded";
        }

        // ============================================================
        // 3. Controlled concurrency
        // ============================================================

        private static async Task DemonstrateControlledConcurrencyAsync()
        {
            Console.WriteLine("3. CONTROLLED CONCURRENCY");
            Console.WriteLine("----------------------------------------");

            List<int> orders =
                Enumerable.Range(1, 20).ToList();

            int activeOperations = 0;
            int maximumObservedConcurrency = 0;

            await Parallel.ForEachAsync(
                orders,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = 4
                },
                async (orderId, cancellationToken) =>
                {
                    int current =
                        Interlocked.Increment(
                            ref activeOperations);

                    UpdateMaximum(
                        ref maximumObservedConcurrency,
                        current);

                    try
                    {
                        Console.WriteLine(
                            $"Processing order {orderId}. " +
                            $"Active operations: {current}");

                        await Task.Delay(
                            300,
                            cancellationToken);
                    }
                    finally
                    {
                        Interlocked.Decrement(
                            ref activeOperations);
                    }
                });

            Console.WriteLine(
                $"Maximum observed concurrency: " +
                $"{maximumObservedConcurrency}");

            Console.WriteLine(
                "MaxDegreeOfParallelism prevents unlimited concurrent operations.");
        }

        private static void UpdateMaximum(
            ref int target,
            int value)
        {
            int current;

            do
            {
                current = Volatile.Read(ref target);

                if (value <= current)
                    return;
            }
            while (
                Interlocked.CompareExchange(
                    ref target,
                    value,
                    current) != current);
        }

        // ============================================================
        // 4. Thread-safe shared state
        // ============================================================

        private static async Task DemonstrateThreadSafeSharedStateAsync()
        {
            Console.WriteLine("4. THREAD-SAFE SHARED STATE");
            Console.WriteLine("----------------------------------------");

            int requestCount = 0;

            Task[] requests =
                Enumerable.Range(1, 100)
                    .Select(_ =>
                        Task.Run(() =>
                        {
                            Interlocked.Increment(
                                ref requestCount);
                        }))
                    .ToArray();

            await Task.WhenAll(requests);

            Console.WriteLine(
                $"Request count: {requestCount}");

            Console.WriteLine(
                "Interlocked provides an atomic increment for the counter.");
        }

        // ============================================================
        // 5. ConcurrentDictionary
        // ============================================================

        private static async Task DemonstrateConcurrentDictionaryAsync()
        {
            Console.WriteLine("5. CONCURRENT DICTIONARY");
            Console.WriteLine("----------------------------------------");

            ConcurrentDictionary<int, string> cache =
                new();

            Task[] operations =
                Enumerable.Range(1, 20)
                    .Select(orderId =>
                        Task.Run(() =>
                        {
                            cache.TryAdd(
                                orderId,
                                $"Order-{orderId}");
                        }))
                    .ToArray();

            await Task.WhenAll(operations);

            Console.WriteLine(
                $"Items in cache: {cache.Count}");

            if (cache.TryGetValue(
                10,
                out string? order))
            {
                Console.WriteLine(
                    $"Order 10: {order}");
            }

            Console.WriteLine(
                "ConcurrentDictionary provides thread-safe collection operations.");
            Console.WriteLine(
                "It does not automatically make multi-step business workflows atomic.");
        }

        // ============================================================
        // 6. Cancellation
        // ============================================================

        private static async Task DemonstrateCancellationAsync()
        {
            Console.WriteLine("6. REQUEST CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts =
                new();

            cts.CancelAfter(
                TimeSpan.FromMilliseconds(800));

            try
            {
                await LongRunningOperationAsync(
                    cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Operation was cancelled.");
            }

            Console.WriteLine(
                "CancellationToken allows cancellation to propagate through async operations.");
        }

        private static async Task LongRunningOperationAsync(
            CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Console.WriteLine(
                    $"Processing step {i}");

                await Task.Delay(
                    300,
                    cancellationToken);
            }
        }

        // ============================================================
        // 7. Request-scoped state
        // ============================================================

        private static async Task DemonstrateRequestScopedStateAsync()
        {
            Console.WriteLine("7. REQUEST-SCOPED STATE");
            Console.WriteLine("----------------------------------------");

            Task requestA =
                SimulateRequestAsync(
                    "Request-A");

            Task requestB =
                SimulateRequestAsync(
                    "Request-B");

            Task requestC =
                SimulateRequestAsync(
                    "Request-C");

            await Task.WhenAll(
                requestA,
                requestB,
                requestC);

            Console.WriteLine(
                "Each simulated request owns its local state.");
        }

        private static async Task SimulateRequestAsync(
            string requestName)
        {
            // This variable belongs only to this request execution.
            int requestCounter = 0;

            requestCounter++;

            await Task.Delay(200);

            Console.WriteLine(
                $"{requestName}: local counter = {requestCounter}");
        }

        // ============================================================
        // 8. Producer-Consumer using Channel
        // ============================================================

        private static async Task DemonstrateProducerConsumerAsync()
        {
            Console.WriteLine("8. PRODUCER-CONSUMER");
            Console.WriteLine("----------------------------------------");

            System.Threading.Channels.Channel<int> channel =
                System.Threading.Channels.Channel.CreateBounded<int>(
                    5);

            Task producer =
                ProduceOrdersAsync(
                    channel.Writer);

            Task consumer1 =
                ConsumeOrdersAsync(
                    "Consumer-1",
                    channel.Reader);

            Task consumer2 =
                ConsumeOrdersAsync(
                    "Consumer-2",
                    channel.Reader);

            await Task.WhenAll(
                producer,
                consumer1,
                consumer2);

            Console.WriteLine(
                "Producer-consumer processing completed.");
        }

        private static async Task ProduceOrdersAsync(
            System.Threading.Channels.ChannelWriter<int> writer)
        {
            try
            {
                for (int orderId = 1; orderId <= 10; orderId++)
                {
                    await writer.WriteAsync(
                        orderId);

                    Console.WriteLine(
                        $"Produced order: {orderId}");

                    await Task.Delay(100);
                }
            }
            finally
            {
                writer.TryComplete();
            }
        }

        private static async Task ConsumeOrdersAsync(
            string consumerName,
            System.Threading.Channels.ChannelReader<int> reader)
        {
            await foreach (int orderId in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"{consumerName} processing order {orderId}");

                await Task.Delay(250);
            }
        }

        // ============================================================
        // 9. Database concurrency concept
        // ============================================================

        private static async Task DemonstrateDatabaseConcurrencyConceptAsync()
        {
            Console.WriteLine("9. DATABASE CONCURRENCY");
            Console.WriteLine("----------------------------------------");

            Product product =
                new(
                    Id: 101,
                    Stock: 10,
                    Version: 5);

            Console.WriteLine(
                $"Initial stock: {product.Stock}");

            Console.WriteLine(
                $"Initial version: {product.Version}");

            bool firstUpdate =
                TryUpdateStock(
                    product,
                    expectedVersion: 5,
                    newStock: 9);

            Console.WriteLine(
                $"First update successful: {firstUpdate}");

            bool secondUpdate =
                TryUpdateStock(
                    product,
                    expectedVersion: 5,
                    newStock: 8);

            Console.WriteLine(
                $"Second update successful: {secondUpdate}");

            Console.WriteLine(
                $"Final stock: {product.Stock}");

            Console.WriteLine(
                $"Final version: {product.Version}");

            Console.WriteLine(
                "This simulates optimistic concurrency using a version value.");

            await Task.CompletedTask;
        }

        private static bool TryUpdateStock(
            Product product,
            int expectedVersion,
            int newStock)
        {
            if (product.Version != expectedVersion)
            {
                return false;
            }

            product.Stock = newStock;
            product.Version++;

            return true;
        }

        // ============================================================
        // Supporting model
        // ============================================================

        private sealed class Product
        {
            public int Id { get; }

            public int Stock { get; set; }

            public int Version { get; set; }

            public Product(
                int Id,
                int Stock,
                int Version)
            {
                this.Id = Id;
                this.Stock = Stock;
                this.Version = Version;
            }
        }
    }
}
