using System.Diagnostics;

namespace InterviewPrep.CSharp.MultiThreading._02_Concurrency_Parallelism
{
    public static class Concurrency_vs_Parallelism
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("CONCURRENCY VS PARALLELISM");
            Console.WriteLine("========================================");

            await DemonstrateConcurrencyAsync();

            Console.WriteLine();

            await DemonstrateSequentialAsync();

            Console.WriteLine();

            await DemonstrateConcurrentAsync();

            Console.WriteLine();

            DemonstrateParallelism();

            Console.WriteLine();

            DemonstrateThreadVsTask();

            Console.WriteLine();
            Console.WriteLine("All demonstrations completed.");
        }


        // ============================================================
        // 1. CONCURRENCY
        // ============================================================

        private static async Task DemonstrateConcurrencyAsync()
        {
            Console.WriteLine("1. CONCURRENCY");
            Console.WriteLine("----------------------------------------");

            Task task1 = SimulateIoOperationAsync("Operation A", 2000);
            Task task2 = SimulateIoOperationAsync("Operation B", 1500);
            Task task3 = SimulateIoOperationAsync("Operation C", 1000);

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine("All concurrent operations completed.");
        }


        private static async Task SimulateIoOperationAsync(
            string operationName,
            int delayMilliseconds)
        {
            Console.WriteLine(
                $"{operationName} started | Thread: {Environment.CurrentManagedThreadId}");

            // Simulates asynchronous I/O.
            await Task.Delay(delayMilliseconds);

            Console.WriteLine(
                $"{operationName} completed | Thread: {Environment.CurrentManagedThreadId}");
        }


        // ============================================================
        // 2. SEQUENTIAL EXECUTION
        // ============================================================

        private static async Task DemonstrateSequentialAsync()
        {
            Console.WriteLine("2. SEQUENTIAL EXECUTION");
            Console.WriteLine("----------------------------------------");

            Stopwatch stopwatch = Stopwatch.StartNew();

            await SimulateIoOperationAsync("Operation A", 2000);
            await SimulateIoOperationAsync("Operation B", 1500);
            await SimulateIoOperationAsync("Operation C", 1000);

            stopwatch.Stop();

            Console.WriteLine(
                $"Sequential execution time: {stopwatch.ElapsedMilliseconds} ms");
        }


        // ============================================================
        // 3. CONCURRENT EXECUTION
        // ============================================================

        private static async Task DemonstrateConcurrentAsync()
        {
            Console.WriteLine("3. CONCURRENT EXECUTION");
            Console.WriteLine("----------------------------------------");

            Stopwatch stopwatch = Stopwatch.StartNew();

            Task operationA = SimulateIoOperationAsync("Operation A", 2000);
            Task operationB = SimulateIoOperationAsync("Operation B", 1500);
            Task operationC = SimulateIoOperationAsync("Operation C", 1000);

            await Task.WhenAll(operationA, operationB, operationC);

            stopwatch.Stop();

            Console.WriteLine(
                $"Concurrent execution time: {stopwatch.ElapsedMilliseconds} ms");
        }


        // ============================================================
        // 4. PARALLELISM
        // ============================================================

        private static void DemonstrateParallelism()
        {
            Console.WriteLine("4. PARALLELISM");
            Console.WriteLine("----------------------------------------");

            Parallel.For(0, 8, i =>
            {
                Console.WriteLine(
                    $"Processing item {i} | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");

                PerformCpuWork();
            });

            Console.WriteLine("Parallel processing completed.");
        }


        private static void PerformCpuWork()
        {
            double result = 0;

            for (int i = 0; i < 5_000_000; i++)
            {
                result += Math.Sqrt(i);
            }
        }


        // ============================================================
        // 5. THREAD VS TASK
        // ============================================================

        private static void DemonstrateThreadVsTask()
        {
            Console.WriteLine("5. THREAD VS TASK");
            Console.WriteLine("----------------------------------------");

            Thread thread = new Thread(() =>
            {
                Console.WriteLine(
                    $"Manual Thread | Thread ID: " +
                    $"{Environment.CurrentManagedThreadId}");
            });

            thread.Start();
            thread.Join();


            Task task = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task | Thread ID: " +
                    $"{Environment.CurrentManagedThreadId}");
            });

            task.Wait();

            Console.WriteLine();
            Console.WriteLine("Thread = lower-level execution mechanism");
            Console.WriteLine("Task   = higher-level abstraction for work");
        }
    }
}
