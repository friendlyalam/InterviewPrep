namespace InterviewPrep.CSharp.MultiThreading._13_ThreadPool_Starvation
{
    public static class ThreadPool_Starvation
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("THREADPOOL STARVATION");
            Console.WriteLine("========================================");

            await DemonstrateBlockingWorkAsync();

            Console.WriteLine();

            await DemonstrateAsyncWorkAsync();

            Console.WriteLine();

            DemonstrateThreadPoolInformation();

            Console.WriteLine();

            Console.WriteLine("All ThreadPool starvation demonstrations completed.");
        }

        private static async Task DemonstrateBlockingWorkAsync()
        {
            Console.WriteLine("1. BLOCKING THREADPOOL WORK");
            Console.WriteLine("----------------------------------------");

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskNumber = i + 1;

                tasks[i] = Task.Run(() =>
                {
                    Console.WriteLine(
                        $"Task {taskNumber} started | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    // Blocks a ThreadPool thread.
                    Thread.Sleep(1000);

                    Console.WriteLine(
                        $"Task {taskNumber} completed | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                "Blocking work completed.");
        }

        private static async Task DemonstrateAsyncWorkAsync()
        {
            Console.WriteLine("2. ASYNC NON-BLOCKING WORK");
            Console.WriteLine("----------------------------------------");

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskNumber = i + 1;

                tasks[i] = ProcessAsync(
                    taskNumber);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                "Async work completed without blocking threads during the delay.");
        }

        private static async Task ProcessAsync(
            int taskNumber)
        {
            Console.WriteLine(
                $"Task {taskNumber} started | " +
                $"Thread: {Environment.CurrentManagedThreadId}");

            await Task.Delay(1000);

            Console.WriteLine(
                $"Task {taskNumber} completed | " +
                $"Thread: {Environment.CurrentManagedThreadId}");
        }

        private static void DemonstrateThreadPoolInformation()
        {
            Console.WriteLine("3. THREADPOOL INFORMATION");
            Console.WriteLine("----------------------------------------");

            ThreadPool.GetAvailableThreads(
                out int availableWorkerThreads,
                out int availableIoThreads);

            ThreadPool.GetMaxThreads(
                out int maxWorkerThreads,
                out int maxIoThreads);

            Console.WriteLine(
                $"Available Worker Threads: {availableWorkerThreads}");

            Console.WriteLine(
                $"Maximum Worker Threads:  {maxWorkerThreads}");

            Console.WriteLine(
                $"Available IO Threads:     {availableIoThreads}");

            Console.WriteLine(
                $"Maximum IO Threads:      {maxIoThreads}");
        }
    }
}
