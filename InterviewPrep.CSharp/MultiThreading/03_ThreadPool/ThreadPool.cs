using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._03_ThreadPool
{
    public static class ThreadPoolDemo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("THREADPOOL DEMONSTRATION");
            Console.WriteLine("========================================");

            DemonstrateQueueUserWorkItem();

            Console.WriteLine();

            await DemonstrateTaskRun();

            Console.WriteLine();

            DemonstrateThreadPoolInformation();

            Console.WriteLine();

            DemonstrateThreadReuse();

            Console.WriteLine();

            Console.WriteLine("All ThreadPool demonstrations completed.");
        }

        // ------------------------------------------------------------
        // 1. Queue work directly to the ThreadPool
        // ------------------------------------------------------------
        private static void DemonstrateQueueUserWorkItem()
        {
            Console.WriteLine("1. ThreadPool.QueueUserWorkItem");
            Console.WriteLine("----------------------------------------");

            using ManualResetEventSlim completed = new(false);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine(
                    $"Work started | Thread: {Environment.CurrentManagedThreadId}");

                Thread.Sleep(1000);

                Console.WriteLine(
                    $"Work completed | Thread: {Environment.CurrentManagedThreadId}");

                completed.Set();
            });

            completed.Wait();

            Console.WriteLine("ThreadPool work finished.");
        }

        // ------------------------------------------------------------
        // 2. Task.Run normally uses ThreadPool infrastructure
        //    for synchronous CPU-bound work
        // ------------------------------------------------------------
        private static async Task DemonstrateTaskRun()
        {
            Console.WriteLine("2. Task.Run");
            Console.WriteLine("----------------------------------------");

            Task task = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task started | Thread: {Environment.CurrentManagedThreadId}");

                PerformCpuWork();

                Console.WriteLine(
                    $"Task completed | Thread: {Environment.CurrentManagedThreadId}");
            });

            await task;

            Console.WriteLine("Task.Run work finished.");
        }

        // ------------------------------------------------------------
        // 3. View ThreadPool configuration
        // ------------------------------------------------------------
        private static void DemonstrateThreadPoolInformation()
        {
            Console.WriteLine("3. ThreadPool Information");
            Console.WriteLine("----------------------------------------");

            ThreadPool.GetMinThreads(
                out int minimumWorkerThreads,
                out int minimumCompletionPortThreads);

            ThreadPool.GetMaxThreads(
                out int maximumWorkerThreads,
                out int maximumCompletionPortThreads);

            ThreadPool.GetAvailableThreads(
                out int availableWorkerThreads,
                out int availableCompletionPortThreads);

            Console.WriteLine(
                $"Minimum Worker Threads: {minimumWorkerThreads}");

            Console.WriteLine(
                $"Maximum Worker Threads: {maximumWorkerThreads}");

            Console.WriteLine(
                $"Available Worker Threads: {availableWorkerThreads}");

            Console.WriteLine(
                $"Minimum IO Completion Threads: " +
                $"{minimumCompletionPortThreads}");

            Console.WriteLine(
                $"Maximum IO Completion Threads: " +
                $"{maximumCompletionPortThreads}");

            Console.WriteLine(
                $"Available IO Completion Threads: " +
                $"{availableCompletionPortThreads}");
        }

        // ------------------------------------------------------------
        // 4. ThreadPool threads are reusable
        // ------------------------------------------------------------
        private static void DemonstrateThreadReuse()
        {
            Console.WriteLine("4. ThreadPool Thread Reuse");
            Console.WriteLine("----------------------------------------");

            using CountdownEvent completed = new(5);

            for (int i = 1; i <= 5; i++)
            {
                int workItemNumber = i;

                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.WriteLine(
                        $"Work Item {workItemNumber} | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    Thread.Sleep(300);

                    completed.Signal();
                });
            }

            completed.Wait();

            Console.WriteLine(
                "All ThreadPool work items completed.");
        }

        // ------------------------------------------------------------
        // CPU-bound demonstration
        // ------------------------------------------------------------
        private static void PerformCpuWork()
        {
            double result = 0;

            for (int i = 0; i < 5_000_000; i++)
            {
                result += Math.Sqrt(i);
            }

            // Prevent the calculation from being optimized away.
            if (result < 0)
            {
                Console.WriteLine(result);
            }
        }
    }
}
