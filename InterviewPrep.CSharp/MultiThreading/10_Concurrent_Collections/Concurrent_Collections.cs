using System.Collections.Concurrent;

namespace InterviewPrep.CSharp.MultiThreading._10_Concurrent_Collections
{
    public static class Concurrent_Collections
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("CONCURRENT COLLECTIONS");
            Console.WriteLine("========================================");

            await DemonstrateConcurrentDictionaryAsync();

            Console.WriteLine();

            DemonstrateConcurrentQueue();

            Console.WriteLine();

            DemonstrateConcurrentStack();

            Console.WriteLine();

            DemonstrateConcurrentBag();

            Console.WriteLine();

            await DemonstrateBlockingCollectionAsync();

            Console.WriteLine();

            await DemonstrateConcurrentDictionaryAtomicOperationsAsync();

            Console.WriteLine();

            Console.WriteLine("All Concurrent Collection demonstrations completed.");
        }

        // ============================================================
        // 1. CONCURRENT DICTIONARY
        // ============================================================

        private static async Task DemonstrateConcurrentDictionaryAsync()
        {
            Console.WriteLine("1. CONCURRENT DICTIONARY");
            Console.WriteLine("----------------------------------------");

            ConcurrentDictionary<int, string> users = new();

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                int userId = i + 1;

                tasks[i] = Task.Run(() =>
                {
                    users.TryAdd(
                        userId,
                        $"User {userId}");
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                $"Users added: {users.Count}");

            foreach (KeyValuePair<int, string> user in users)
            {
                Console.WriteLine(
                    $"{user.Key} -> {user.Value}");
            }
        }

        // ============================================================
        // 2. CONCURRENT QUEUE
        // ============================================================

        private static void DemonstrateConcurrentQueue()
        {
            Console.WriteLine("2. CONCURRENT QUEUE");
            Console.WriteLine("----------------------------------------");

            ConcurrentQueue<string> queue = new();

            queue.Enqueue("Order 1");
            queue.Enqueue("Order 2");
            queue.Enqueue("Order 3");

            while (queue.TryDequeue(out string? order))
            {
                Console.WriteLine(
                    $"Processed: {order}");
            }

            Console.WriteLine(
                "ConcurrentQueue provides FIFO behavior.");
        }

        // ============================================================
        // 3. CONCURRENT STACK
        // ============================================================

        private static void DemonstrateConcurrentStack()
        {
            Console.WriteLine("3. CONCURRENT STACK");
            Console.WriteLine("----------------------------------------");

            ConcurrentStack<string> stack = new();

            stack.Push("Task 1");
            stack.Push("Task 2");
            stack.Push("Task 3");

            while (stack.TryPop(out string? task))
            {
                Console.WriteLine(
                    $"Processed: {task}");
            }

            Console.WriteLine(
                "ConcurrentStack provides LIFO behavior.");
        }

        // ============================================================
        // 4. CONCURRENT BAG
        // ============================================================

        private static void DemonstrateConcurrentBag()
        {
            Console.WriteLine("4. CONCURRENT BAG");
            Console.WriteLine("----------------------------------------");

            ConcurrentBag<int> numbers = new();

            Parallel.For(
                0,
                10,
                i =>
                {
                    numbers.Add(i);
                });

            Console.WriteLine(
                $"Items added: {numbers.Count}");

            Console.WriteLine("Items:");

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine(
                "ConcurrentBag does not guarantee ordering.");
        }

        // ============================================================
        // 5. BLOCKING COLLECTION
        // ============================================================

        private static async Task DemonstrateBlockingCollectionAsync()
        {
            Console.WriteLine("5. BLOCKING COLLECTION");
            Console.WriteLine("----------------------------------------");

            using BlockingCollection<int> queue =
                new(
                    new ConcurrentQueue<int>(),
                    boundedCapacity: 3);

            Task producer = Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    queue.Add(i);

                    Console.WriteLine(
                        $"Produced: {i}");

                    Thread.Sleep(200);
                }

                queue.CompleteAdding();
            });

            Task consumer = Task.Run(() =>
            {
                foreach (int item in queue.GetConsumingEnumerable())
                {
                    Console.WriteLine(
                        $"Consumed: {item}");

                    Thread.Sleep(500);
                }
            });

            await Task.WhenAll(
                producer,
                consumer);

            Console.WriteLine(
                "Producer-consumer processing completed.");
        }

        // ============================================================
        // 6. CONCURRENT DICTIONARY ATOMIC OPERATIONS
        // ============================================================

        private static async Task DemonstrateConcurrentDictionaryAtomicOperationsAsync()
        {
            Console.WriteLine("6. CONCURRENT DICTIONARY ATOMIC OPERATIONS");
            Console.WriteLine("----------------------------------------");

            ConcurrentDictionary<string, int> counters = new();

            const int taskCount = 10;
            const int incrementsPerTask = 1_000;

            Task[] tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < incrementsPerTask; j++)
                    {
                        counters.AddOrUpdate(
                            "OrdersProcessed",
                            1,
                            (_, currentValue) => currentValue + 1);
                    }
                });
            }

            await Task.WhenAll(tasks);

            counters.TryGetValue(
                "OrdersProcessed",
                out int result);

            int expected =
                taskCount * incrementsPerTask;

            Console.WriteLine(
                $"Expected: {expected}");

            Console.WriteLine(
                $"Actual:   {result}");

            Console.WriteLine(
                "AddOrUpdate provides a thread-safe dictionary update.");
        }
}

//Call from Program.cs:

//await Concurrent_Collections.RunAsync();
}
