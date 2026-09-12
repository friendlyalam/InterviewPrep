

namespace InterviewPrep.CSharp.MultiThreading._09_Atomic_Operations_Memory
{
    public static class Atomic_Operations
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("ATOMIC OPERATIONS");
            Console.WriteLine("========================================");

            await DemonstrateNonAtomicIncrementAsync();

            Console.WriteLine();

            await DemonstrateInterlockedIncrementAsync();

            Console.WriteLine();

            DemonstrateInterlockedAdd();

            Console.WriteLine();

            DemonstrateInterlockedDecrement();

            Console.WriteLine();

            DemonstrateInterlockedExchange();

            Console.WriteLine();

            DemonstrateInterlockedCompareExchange();

            Console.WriteLine();

            await DemonstrateAtomicRequestCounterAsync();

            Console.WriteLine();

            DemonstrateVolatileFlag();

            Console.WriteLine();

            Console.WriteLine("All atomic operation demonstrations completed.");
        }

        // ============================================================
        // 1. NON-ATOMIC OPERATION
        // ============================================================

        private static async Task DemonstrateNonAtomicIncrementAsync()
        {
            Console.WriteLine("1. NON-ATOMIC INCREMENT");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            const int taskCount = 10;
            const int incrementsPerTask = 10_000;

            Task[] tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < incrementsPerTask; j++)
                    {
                        counter++;
                    }
                });
            }

            await Task.WhenAll(tasks);

            int expected = taskCount * incrementsPerTask;

            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual:   {counter}");

            Console.WriteLine(
                "counter++ is a read-modify-write operation and is not atomic.");
        }

        // ============================================================
        // 2. INTERLOCKED.INCREMENT
        // ============================================================

        private static async Task DemonstrateInterlockedIncrementAsync()
        {
            Console.WriteLine("2. INTERLOCKED.INCREMENT");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            const int taskCount = 10;
            const int incrementsPerTask = 10_000;

            Task[] tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < incrementsPerTask; j++)
                    {
                        Interlocked.Increment(ref counter);
                    }
                });
            }

            await Task.WhenAll(tasks);

            int expected = taskCount * incrementsPerTask;

            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual:   {counter}");

            Console.WriteLine(
                "Interlocked.Increment performs an atomic increment.");
        }

        // ============================================================
        // 3. INTERLOCKED.ADD
        // ============================================================

        private static void DemonstrateInterlockedAdd()
        {
            Console.WriteLine("3. INTERLOCKED.ADD");
            Console.WriteLine("----------------------------------------");

            int total = 100;

            int result = Interlocked.Add(
                ref total,
                50);

            Console.WriteLine($"Original value: 100");
            Console.WriteLine($"Added value:    50");
            Console.WriteLine($"Final value:    {total}");
            Console.WriteLine($"Returned value: {result}");
        }

        // ============================================================
        // 4. INTERLOCKED.DECREMENT
        // ============================================================

        private static void DemonstrateInterlockedDecrement()
        {
            Console.WriteLine("4. INTERLOCKED.DECREMENT");
            Console.WriteLine("----------------------------------------");

            int activeWorkers = 5;

            int result = Interlocked.Decrement(
                ref activeWorkers);

            Console.WriteLine($"Initial workers: 5");
            Console.WriteLine($"Final workers:   {activeWorkers}");
            Console.WriteLine($"Returned value:  {result}");
        }

        // ============================================================
        // 5. INTERLOCKED.EXCHANGE
        // ============================================================

        private static void DemonstrateInterlockedExchange()
        {
            Console.WriteLine("5. INTERLOCKED.EXCHANGE");
            Console.WriteLine("----------------------------------------");

            int status = 0;

            int previousValue = Interlocked.Exchange(
                ref status,
                1);

            Console.WriteLine(
                $"Previous status: {previousValue}");

            Console.WriteLine(
                $"Current status:  {status}");

            Console.WriteLine(
                "Exchange atomically replaces the previous value.");
        }

        // ============================================================
        // 6. INTERLOCKED.COMPAREEXCHANGE
        // ============================================================

        private static void DemonstrateInterlockedCompareExchange()
        {
            Console.WriteLine("6. INTERLOCKED.COMPAREEXCHANGE");
            Console.WriteLine("----------------------------------------");

            int status = 0;

            int originalValue = Interlocked.CompareExchange(
                ref status,
                1,
                0);

            Console.WriteLine(
                $"Original value returned: {originalValue}");

            Console.WriteLine(
                $"Current status:          {status}");

            Console.WriteLine();

            int secondResult = Interlocked.CompareExchange(
                ref status,
                2,
                0);

            Console.WriteLine(
                $"Second returned value:   {secondResult}");

            Console.WriteLine(
                $"Current status:          {status}");

            Console.WriteLine(
                "The second update does not happen because "
                + "the current value is no longer 0.");
        }

        // ============================================================
        // 7. REAL-WORLD REQUEST COUNTER
        // ============================================================

        private static async Task DemonstrateAtomicRequestCounterAsync()
        {
            Console.WriteLine("7. REAL-WORLD REQUEST COUNTER");
            Console.WriteLine("----------------------------------------");

            int processedRequests = 0;

            const int requestCount = 20;

            Task[] requests = new Task[requestCount];

            for (int i = 0; i < requestCount; i++)
            {
                requests[i] = Task.Run(() =>
                {
                    ProcessRequest();

                    Interlocked.Increment(
                        ref processedRequests);
                });
            }

            await Task.WhenAll(requests);

            Console.WriteLine(
                $"Expected processed requests: {requestCount}");

            Console.WriteLine(
                $"Actual processed requests:   {processedRequests}");

            Console.WriteLine(
                "Interlocked is suitable for simple thread-safe counters.");
        }

        private static void ProcessRequest()
        {
            Thread.Sleep(50);
        }

        // ============================================================
        // 8. VOLATILE FLAG
        // ============================================================

        private static void DemonstrateVolatileFlag()
        {
            Console.WriteLine("8. VOLATILE FLAG");
            Console.WriteLine("----------------------------------------");

            Worker worker = new();

            worker.Start();

            Thread.Sleep(500);

            Console.WriteLine("Requesting worker shutdown...");

            worker.Stop();

            worker.WaitForCompletion();

            Console.WriteLine("Worker stopped.");
        }

        // ============================================================
        // HELPER CLASS FOR VOLATILE DEMONSTRATION
        // ============================================================

        private sealed class Worker
        {
            private volatile bool _stopRequested;

            private Thread? _thread;

            public void Start()
            {
                _thread = new Thread(Run);

                _thread.Start();
            }

            public void Stop()
            {
                _stopRequested = true;
            }

            public void WaitForCompletion()
            {
                _thread?.Join();
            }

            private void Run()
            {
                Console.WriteLine("Worker started.");

                while (!_stopRequested)
                {
                    Console.WriteLine("Worker is processing...");

                    Thread.Sleep(100);
                }

                Console.WriteLine(
                    "Worker detected the stop request.");
            }
        }
    }
}

//Call it from Program.cs:

//await Atomic_Operations.RunAsync();
