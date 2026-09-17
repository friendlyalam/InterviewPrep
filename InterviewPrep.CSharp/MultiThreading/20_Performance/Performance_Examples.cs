using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._20_Performance
{
    public static class Performance_Examples
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("CONCURRENCY PERFORMANCE EXAMPLES");
            Console.WriteLine("========================================");

            await DemonstrateSequentialVsConcurrentAsync();

            Console.WriteLine();

            await DemonstrateControlledConcurrencyAsync();

            Console.WriteLine();

            await DemonstrateSemaphoreSlimConcurrencyLimitAsync();

            Console.WriteLine();

            DemonstrateInterlockedVsLock();

            Console.WriteLine();

            await DemonstrateProducerConsumerWithBackpressureAsync();

            Console.WriteLine();

            await DemonstrateCancellationAndTimeoutAsync();

            Console.WriteLine();

            DemonstrateParallelCpuWork();

            Console.WriteLine();

            await DemonstrateBatchProcessingAsync();

            Console.WriteLine();

            DemonstratePerformanceMeasurement();

            Console.WriteLine();

            Console.WriteLine("All performance demonstrations completed.");
        }

        // ============================================================
        // 1. Sequential vs Concurrent I/O
        // ============================================================

        private static async Task DemonstrateSequentialVsConcurrentAsync()
        {
            Console.WriteLine("1. SEQUENTIAL VS CONCURRENT I/O");
            Console.WriteLine("----------------------------------------");

            Stopwatch stopwatch =
                Stopwatch.StartNew();

            string product =
                await GetProductAsync();

            string inventory =
                await GetInventoryAsync();

            string payment =
                await GetPaymentAsync();

            stopwatch.Stop();

            Console.WriteLine("Sequential:");
            Console.WriteLine($"Product:   {product}");
            Console.WriteLine($"Inventory: {inventory}");
            Console.WriteLine($"Payment:   {payment}");
            Console.WriteLine($"Elapsed:   {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine();

            stopwatch.Restart();

            Task<string> productTask =
                GetProductAsync();

            Task<string> inventoryTask =
                GetInventoryAsync();

            Task<string> paymentTask =
                GetPaymentAsync();

            await Task.WhenAll(
                productTask,
                inventoryTask,
                paymentTask);

            stopwatch.Stop();

            Console.WriteLine("Concurrent:");
            Console.WriteLine($"Product:   {await productTask}");
            Console.WriteLine($"Inventory: {await inventoryTask}");
            Console.WriteLine($"Payment:   {await paymentTask}");
            Console.WriteLine($"Elapsed:   {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine();
            Console.WriteLine(
                "Independent I/O operations can make progress concurrently.");
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

        private static async Task<string> GetPaymentAsync()
        {
            await Task.Delay(400);

            return "Payment status loaded";
        }

        // ============================================================
        // 2. Controlled concurrency using Parallel.ForEachAsync
        // ============================================================

        private static async Task DemonstrateControlledConcurrencyAsync()
        {
            Console.WriteLine("2. CONTROLLED CONCURRENCY");
            Console.WriteLine("----------------------------------------");

            int activeOperations = 0;
            int maximumObservedConcurrency = 0;

            int[] orders =
                Enumerable.Range(1, 20)
                    .ToArray();

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
                            $"Active: {current}");

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

            Console.WriteLine();

            Console.WriteLine(
                $"Maximum observed concurrency: " +
                $"{maximumObservedConcurrency}");

            Console.WriteLine(
                "Concurrency was intentionally limited to 4.");
        }

        private static void UpdateMaximum(
            ref int target,
            int value)
        {
            int current;

            do
            {
                current =
                    Volatile.Read(
                        ref target);

                if (value <= current)
                {
                    return;
                }
            }
            while (
                Interlocked.CompareExchange(
                    ref target,
                    value,
                    current) != current);
        }

        // ============================================================
        // 3. SemaphoreSlim concurrency limit
        // ============================================================

        private static async Task DemonstrateSemaphoreSlimConcurrencyLimitAsync()
        {
            Console.WriteLine("3. SEMAPHORESLIM CONCURRENCY LIMIT");
            Console.WriteLine("----------------------------------------");

            using SemaphoreSlim semaphore =
                new(3);

            int activeOperations = 0;
            int maximumObservedConcurrency = 0;

            Task[] tasks =
                Enumerable.Range(1, 10)
                    .Select(
                        orderId =>
                            ProcessWithSemaphoreAsync(
                                orderId,
                                semaphore,
                                () =>
                                {
                                    int current =
                                        Interlocked.Increment(
                                            ref activeOperations);

                                    UpdateMaximum(
                                        ref maximumObservedConcurrency,
                                        current);
                                },
                                () =>
                                {
                                    Interlocked.Decrement(
                                        ref activeOperations);
                                }))
                    .ToArray();

            await Task.WhenAll(tasks);

            Console.WriteLine();

            Console.WriteLine(
                $"Maximum observed concurrency: " +
                $"{maximumObservedConcurrency}");

            Console.WriteLine(
                "SemaphoreSlim limited the protected operation to 3 concurrent executions.");
        }

        private static async Task ProcessWithSemaphoreAsync(
            int orderId,
            SemaphoreSlim semaphore,
            Action onEntered,
            Action onExited)
        {
            await semaphore.WaitAsync();

            try
            {
                onEntered();

                Console.WriteLine(
                    $"Order {orderId} entered the limited section.");

                await Task.Delay(500);
            }
            finally
            {
                onExited();

                semaphore.Release();
            }
        }

        // ============================================================
        // 4. Interlocked vs lock
        // ============================================================

        private static void DemonstrateInterlockedVsLock()
        {
            Console.WriteLine("4. INTERLOCKED VS LOCK");
            Console.WriteLine("----------------------------------------");

            const int iterations = 100_000;

            int interlockedCounter = 0;

            Stopwatch stopwatch =
                Stopwatch.StartNew();

            Parallel.For(
                0,
                iterations,
                _ =>
                {
                    Interlocked.Increment(
                        ref interlockedCounter);
                });

            stopwatch.Stop();

            Console.WriteLine(
                $"Interlocked counter: {interlockedCounter}");

            Console.WriteLine(
                $"Interlocked elapsed: {stopwatch.ElapsedTicks} ticks");

            int lockCounter = 0;

            object syncObject =
                new();

            stopwatch.Restart();

            Parallel.For(
                0,
                iterations,
                _ =>
                {
                    lock (syncObject)
                    {
                        lockCounter++;
                    }
                });

            stopwatch.Stop();

            Console.WriteLine(
                $"Lock counter: {lockCounter}");

            Console.WriteLine(
                $"Lock elapsed: {stopwatch.ElapsedTicks} ticks");

            Console.WriteLine();

            Console.WriteLine(
                "Interlocked is useful for simple atomic operations.");
            Console.WriteLine(
                "Do not replace locks blindly; choose the simplest correct synchronization mechanism.");
        }

        // ============================================================
        // 5. Producer-consumer + bounded Channel
        // ============================================================

        private static async Task DemonstrateProducerConsumerWithBackpressureAsync()
        {
            Console.WriteLine("5. PRODUCER-CONSUMER + BACKPRESSURE");
            Console.WriteLine("----------------------------------------");

            Channel<int> channel =
                Channel.CreateBounded<int>(
                    new BoundedChannelOptions(5)
                    {
                        FullMode =
                            BoundedChannelFullMode.Wait
                    });

            Task producer =
                ProduceAsync(
                    channel.Writer);

            Task consumer1 =
                ConsumeAsync(
                    "Consumer-1",
                    channel.Reader);

            Task consumer2 =
                ConsumeAsync(
                    "Consumer-2",
                    channel.Reader);

            await Task.WhenAll(
                producer,
                consumer1,
                consumer2);

            Console.WriteLine();

            Console.WriteLine(
                "Bounded Channel provides backpressure when consumers cannot keep up.");
        }

        private static async Task ProduceAsync(
            ChannelWriter<int> writer)
        {
            try
            {
                for (int orderId = 1; orderId <= 15; orderId++)
                {
                    await writer.WriteAsync(
                        orderId);

                    Console.WriteLine(
                        $"Produced: {orderId}");

                    await Task.Delay(50);
                }
            }
            finally
            {
                writer.TryComplete();
            }
        }

        private static async Task ConsumeAsync(
            string consumerName,
            ChannelReader<int> reader)
        {
            await foreach (int orderId in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"{consumerName} processing {orderId}");

                await Task.Delay(200);
            }
        }

        // ============================================================
        // 6. Cancellation + timeout
        // ============================================================

        private static async Task DemonstrateCancellationAndTimeoutAsync()
        {
            Console.WriteLine("6. CANCELLATION + TIMEOUT");
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
                    "Long-running operation was cancelled.");
            }

            Console.WriteLine();

            try
            {
                await SlowOperationAsync()
                    .WaitAsync(
                        TimeSpan.FromMilliseconds(500));

                Console.WriteLine(
                    "Operation completed within timeout.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine(
                    "Operation exceeded the timeout.");
            }

            Console.WriteLine(
                "Cancellation and timeouts prevent resources from waiting indefinitely.");
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
                    250,
                    cancellationToken);
            }
        }

        private static async Task SlowOperationAsync()
        {
            await Task.Delay(2000);
        }

        // ============================================================
        // 7. CPU-bound parallel processing
        // ============================================================

        private static void DemonstrateParallelCpuWork()
        {
            Console.WriteLine("7. CPU-BOUND PARALLEL PROCESSING");
            Console.WriteLine("----------------------------------------");

            const int itemCount = 100;

            long sequentialResult = 0;

            Stopwatch stopwatch =
                Stopwatch.StartNew();

            for (int i = 1; i <= itemCount; i++)
            {
                sequentialResult +=
                    ExpensiveCalculation(i);
            }

            stopwatch.Stop();

            Console.WriteLine(
                $"Sequential result: {sequentialResult}");

            Console.WriteLine(
                $"Sequential elapsed: {stopwatch.ElapsedMilliseconds} ms");

            long parallelResult = 0;

            stopwatch.Restart();

            Parallel.For(
                1,
                itemCount + 1,
                i =>
                {
                    long result =
                        ExpensiveCalculation(i);

                    Interlocked.Add(
                        ref parallelResult,
                        result);
                });

            stopwatch.Stop();

            Console.WriteLine(
                $"Parallel result:   {parallelResult}");

            Console.WriteLine(
                $"Parallel elapsed:  {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine();

            Console.WriteLine(
                "Parallelism can improve CPU-bound workloads when enough independent work exists.");
            Console.WriteLine(
                "Actual improvement depends on CPU cores and workload characteristics.");
        }

        private static long ExpensiveCalculation(
            int value)
        {
            long result = 0;

            for (int i = 0; i < 100_000; i++)
            {
                result +=
                    (value * 31L + i) % 97;
            }

            return result;
        }

        // ============================================================
        // 8. Batch processing
        // ============================================================

        private static async Task DemonstrateBatchProcessingAsync()
        {
            Console.WriteLine("8. BATCH PROCESSING");
            Console.WriteLine("----------------------------------------");

            int[] orderIds =
                Enumerable.Range(1, 25)
                    .ToArray();

            const int batchSize = 5;

            foreach (int[] batch in
                     orderIds.Chunk(batchSize))
            {
                Console.WriteLine(
                    $"Processing batch: " +
                    $"{string.Join(", ", batch)}");

                await ProcessBatchAsync(
                    batch);
            }

            Console.WriteLine();

            Console.WriteLine(
                "Batching can reduce network/database round trips and scheduling overhead.");
        }

        private static async Task ProcessBatchAsync(
            int[] orderIds)
        {
            await Task.Delay(200);

            Console.WriteLine(
                $"Batch completed. Count: {orderIds.Length}");
        }

        // ============================================================
        // 9. Measuring performance
        // ============================================================

        private static void DemonstratePerformanceMeasurement()
        {
            Console.WriteLine("9. PERFORMANCE MEASUREMENT");
            Console.WriteLine("----------------------------------------");

            const int iterations = 1_000_000;

            Stopwatch stopwatch =
                Stopwatch.StartNew();

            long result = 0;

            for (int i = 0; i < iterations; i++)
            {
                result += i;
            }

            stopwatch.Stop();

            Console.WriteLine(
                $"Result: {result}");

            Console.WriteLine(
                $"Elapsed milliseconds: " +
                $"{stopwatch.ElapsedMilliseconds}");

            Console.WriteLine(
                $"Elapsed ticks: " +
                $"{stopwatch.ElapsedTicks}");

            Console.WriteLine();

            Console.WriteLine(
                "For serious .NET microbenchmarks, prefer BenchmarkDotNet.");
            Console.WriteLine(
                "For application performance, combine benchmarks with realistic load testing and production telemetry.");
        }
    }
}
