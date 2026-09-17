using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._14_Parallel_Programming
{
    public static class Parallel_Programming
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("PARALLEL PROGRAMMING");
            Console.WriteLine("========================================");

            DemonstrateParallelFor();

            Console.WriteLine();

            DemonstrateParallelForEach();

            Console.WriteLine();

            DemonstrateMaxDegreeOfParallelism();

            Console.WriteLine();

            DemonstrateThreadSafeCollection();

            Console.WriteLine();

            DemonstrateInterlocked();

            Console.WriteLine();

            await DemonstrateParallelForEachAsync();

            Console.WriteLine();

            await DemonstrateCancellationAsync();

            Console.WriteLine();

            DemonstrateParallelVsSequential();

            Console.WriteLine();

            Console.WriteLine("All Parallel Programming demonstrations completed.");
        }

        private static void DemonstrateParallelFor()
        {
            Console.WriteLine("1. PARALLEL.FOR");
            Console.WriteLine("----------------------------------------");

            Parallel.For(
                0,
                10,
                i =>
                {
                    Console.WriteLine(
                        $"Processing {i} | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    PerformCpuWork();
                });

            Console.WriteLine("Parallel.For completed.");
        }

        private static void DemonstrateParallelForEach()
        {
            Console.WriteLine("2. PARALLEL.FOREACH");
            Console.WriteLine("----------------------------------------");

            int[] numbers =
            {
            1, 2, 3, 4, 5,
            6, 7, 8, 9, 10
        };

            Parallel.ForEach(
                numbers,
                number =>
                {
                    int result = number * number;

                    Console.WriteLine(
                        $"{number}² = {result} | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");
                });

            Console.WriteLine("Parallel.ForEach completed.");
        }

        private static void DemonstrateMaxDegreeOfParallelism()
        {
            Console.WriteLine("3. MAX DEGREE OF PARALLELISM");
            Console.WriteLine("----------------------------------------");

            ParallelOptions options = new()
            {
                MaxDegreeOfParallelism = 2
            };

            Parallel.ForEach(
                Enumerable.Range(1, 8),
                options,
                item =>
                {
                    Console.WriteLine(
                        $"Processing {item} | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    Thread.Sleep(300);
                });

            Console.WriteLine(
                "Maximum concurrent parallel operations were limited to 2.");
        }

        private static void DemonstrateThreadSafeCollection()
        {
            Console.WriteLine("4. THREAD-SAFE COLLECTION");
            Console.WriteLine("----------------------------------------");

            ConcurrentBag<int> results = new();

            Parallel.For(
                1,
                11,
                i =>
                {
                    results.Add(i * i);
                });

            Console.WriteLine(
                $"Results generated: {results.Count}");

            foreach (int result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static void DemonstrateInterlocked()
        {
            Console.WriteLine("5. INTERLOCKED WITH PARALLEL WORK");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            Parallel.For(
                0,
                10_000,
                _ =>
                {
                    Interlocked.Increment(
                        ref counter);
                });

            Console.WriteLine(
                $"Expected: 10000");

            Console.WriteLine(
                $"Actual:   {counter}");

            Console.WriteLine(
                "Interlocked provides an atomic counter update.");
        }

        private static async Task DemonstrateParallelForEachAsync()
        {
            Console.WriteLine("6. PARALLEL.FOREACHASYNC");
            Console.WriteLine("----------------------------------------");

            ParallelOptions options = new()
            {
                MaxDegreeOfParallelism = 3
            };

            await Parallel.ForEachAsync(
                Enumerable.Range(1, 8),
                options,
                async (item, cancellationToken) =>
                {
                    Console.WriteLine(
                        $"Started item {item} | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    await Task.Delay(
                        500,
                        cancellationToken);

                    Console.WriteLine(
                        $"Completed item {item}");
                });

            Console.WriteLine(
                "Parallel.ForEachAsync completed.");
        }

        private static async Task DemonstrateCancellationAsync()
        {
            Console.WriteLine("7. CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts =
                new();

            ParallelOptions options = new()
            {
                MaxDegreeOfParallelism = 4,
                CancellationToken = cts.Token
            };

            Task parallelTask = Task.Run(() =>
            {
                Parallel.For(
                    0,
                    100,
                    options,
                    i =>
                    {
                        options.CancellationToken
                            .ThrowIfCancellationRequested();

                        Thread.Sleep(100);

                        Console.WriteLine(
                            $"Processing {i}");
                    });
            });

            await Task.Delay(500);

            Console.WriteLine(
                "Requesting cancellation...");

            cts.Cancel();

            try
            {
                await parallelTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Parallel operation cancelled.");
            }
        }

        private static void DemonstrateParallelVsSequential()
        {
            Console.WriteLine("8. SEQUENTIAL VS PARALLEL");
            Console.WriteLine("----------------------------------------");

            const int itemCount = 100;

            Stopwatch sequentialWatch =
                Stopwatch.StartNew();

            for (int i = 0; i < itemCount; i++)
            {
                PerformCpuWork();
            }

            sequentialWatch.Stop();

            Stopwatch parallelWatch =
                Stopwatch.StartNew();

            Parallel.For(
                0,
                itemCount,
                _ =>
                {
                    PerformCpuWork();
                });

            parallelWatch.Stop();

            Console.WriteLine(
                $"Sequential: {sequentialWatch.ElapsedMilliseconds} ms");

            Console.WriteLine(
                $"Parallel:   {parallelWatch.ElapsedMilliseconds} ms");

            Console.WriteLine(
                "Actual performance depends on workload, CPU cores, " +
                "parallel overhead, and system load.");
        }

        private static void PerformCpuWork()
        {
            double result = 0;

            for (int i = 0; i < 100_000; i++)
            {
                result += Math.Sqrt(i);
            }

            GC.KeepAlive(result);
        }
    }
}
