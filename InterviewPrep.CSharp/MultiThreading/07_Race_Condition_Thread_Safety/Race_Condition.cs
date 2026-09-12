using System.Collections.Concurrent;

namespace InterviewPrep.CSharp.MultiThreading._07_Race_Condition_Thread_Safety
{
    public static class Race_Condition
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("RACE CONDITION & THREAD SAFETY");
            Console.WriteLine("========================================");

            await DemonstrateRaceConditionAsync();

            Console.WriteLine();

            await DemonstrateLockAsync();

            Console.WriteLine();

            await DemonstrateInterlockedAsync();

            Console.WriteLine();

            await DemonstrateThreadSafeCollectionAsync();

            Console.WriteLine();

            await DemonstrateCheckThenActRaceAsync();

            Console.WriteLine();

            Console.WriteLine("All Race Condition demonstrations completed.");
        }

        private static async Task DemonstrateRaceConditionAsync()
        {
            Console.WriteLine("1. RACE CONDITION");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 10_000; j++)
                    {
                        counter++;
                    }
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                $"Expected: {tasks.Length * 10_000}");

            Console.WriteLine(
                $"Actual:   {counter}");

            Console.WriteLine(
                "The result may be lower than expected because "
                + "counter++ is a read-modify-write operation.");
        }

        private static async Task DemonstrateLockAsync()
        {
            Console.WriteLine("2. FIXING RACE CONDITION WITH LOCK");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            object lockObject = new();

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 10_000; j++)
                    {
                        lock (lockObject)
                        {
                            counter++;
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                $"Expected: {tasks.Length * 10_000}");

            Console.WriteLine(
                $"Actual:   {counter}");

            Console.WriteLine(
                "The critical section is protected by the same lock.");
        }

        private static async Task DemonstrateInterlockedAsync()
        {
            Console.WriteLine("3. FIXING RACE CONDITION WITH INTERLOCKED");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 10_000; j++)
                    {
                        Interlocked.Increment(ref counter);
                    }
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                $"Expected: {tasks.Length * 10_000}");

            Console.WriteLine(
                $"Actual:   {counter}");

            Console.WriteLine(
                "Interlocked.Increment performs an atomic increment.");
        }

        private static async Task DemonstrateThreadSafeCollectionAsync()
        {
            Console.WriteLine("4. THREAD-SAFE COLLECTION");
            Console.WriteLine("----------------------------------------");

            ConcurrentBag<int> numbers = new();

            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskNumber = i;

                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 1_000; j++)
                    {
                        numbers.Add(
                            taskNumber * 1_000 + j);
                    }
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                $"Expected item count: {tasks.Length * 1_000}");

            Console.WriteLine(
                $"Actual item count:   {numbers.Count}");

            Console.WriteLine(
                "ConcurrentBag supports concurrent additions.");
        }

        private static async Task DemonstrateCheckThenActRaceAsync()
        {
            Console.WriteLine("5. CHECK-THEN-ACT RACE CONDITION");
            Console.WriteLine("----------------------------------------");

            int stock = 1;

            Task task1 = Task.Run(() =>
            {
                TryPurchase(
                    "Customer A",
                    ref stock);
            });

            Task task2 = Task.Run(() =>
            {
                TryPurchase(
                    "Customer B",
                    ref stock);
            });

            await Task.WhenAll(
                task1,
                task2);

            Console.WriteLine(
                $"Remaining stock: {stock}");

            Console.WriteLine(
                "A real inventory system should use an "
                + "appropriate concurrency mechanism such as "
                + "an atomic database update or transaction.");
        }

        private static void TryPurchase(
            string customer,
            ref int stock)
        {
            // Intentional demonstration of a check-then-act race.
            if (stock > 0)
            {
                Thread.Sleep(10);

                stock--;

                Console.WriteLine(
                    $"{customer} purchased the item.");
            }
            else
            {
                Console.WriteLine(
                    $"{customer} could not purchase the item.");
            }
        }
    }
}

//Call from Program.cs:

//await Race_Condition.RunAsync();