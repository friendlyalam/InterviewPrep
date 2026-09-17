using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._12_Deadlock
{
    public static class Deadlock
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("DEADLOCK DEMONSTRATION");
            Console.WriteLine("========================================");

            await DemonstrateDeadlockConceptAsync();

            Console.WriteLine();

            DemonstrateDeadlockPrevention();

            Console.WriteLine();

            DemonstrateConsistentLockOrdering();

            Console.WriteLine();

            await DemonstrateSemaphoreSlimPatternAsync();

            Console.WriteLine();

            DemonstrateMonitorTimeout();

            Console.WriteLine();

            Console.WriteLine("All Deadlock demonstrations completed.");
        }

        private static async Task DemonstrateDeadlockConceptAsync()
        {
            Console.WriteLine("1. DEADLOCK CONCEPT");
            Console.WriteLine("----------------------------------------");

            object lockA = new();
            object lockB = new();

            using ManualResetEventSlim task1HasLockA = new(false);
            using ManualResetEventSlim task2HasLockB = new(false);

            Task task1 = Task.Run(() =>
            {
                lock (lockA)
                {
                    Console.WriteLine(
                        $"Task 1 acquired Lock A | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    task1HasLockA.Set();

                    // Wait until Task 2 owns Lock B.
                    task2HasLockB.Wait();

                    Console.WriteLine(
                        "Task 1 waiting for Lock B...");

                    lock (lockB)
                    {
                        Console.WriteLine(
                            "Task 1 acquired Lock B.");
                    }
                }
            });

            Task task2 = Task.Run(() =>
            {
                lock (lockB)
                {
                    Console.WriteLine(
                        $"Task 2 acquired Lock B | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    task2HasLockB.Set();

                    // Wait until Task 1 owns Lock A.
                    task1HasLockA.Wait();

                    Console.WriteLine(
                        "Task 2 waiting for Lock A...");

                    lock (lockA)
                    {
                        Console.WriteLine(
                            "Task 2 acquired Lock A.");
                    }
                }
            });

            Task completedTask = await Task.WhenAny(
                task1,
                task2,
                Task.Delay(2000));

            if (completedTask.IsCompletedSuccessfully)
            {
                Console.WriteLine(
                    "Both tasks completed without deadlock.");
            }
            else
            {
                Console.WriteLine(
                    "Deadlock detected: tasks did not complete within the timeout.");
            }

            // The intentionally deadlocked tasks must not be awaited here.
            // This demonstration uses isolated synchronization objects so
            // they do not affect the remaining examples.
        }

        private static void DemonstrateDeadlockPrevention()
        {
            Console.WriteLine("2. DEADLOCK PREVENTION");
            Console.WriteLine("----------------------------------------");

            object lockA = new();
            object lockB = new();

            Task task1 = Task.Run(() =>
            {
                lock (lockA)
                {
                    Thread.Sleep(50);

                    lock (lockB)
                    {
                        Console.WriteLine(
                            "Task 1 acquired Lock A → Lock B.");
                    }
                }
            });

            Task task2 = Task.Run(() =>
            {
                // Same lock order as Task 1.
                lock (lockA)
                {
                    Thread.Sleep(50);

                    lock (lockB)
                    {
                        Console.WriteLine(
                            "Task 2 acquired Lock A → Lock B.");
                    }
                }
            });

            Task.WaitAll(
                task1,
                task2);

            Console.WriteLine(
                "Both tasks completed because lock ordering is consistent.");
        }

        private static void DemonstrateConsistentLockOrdering()
        {
            Console.WriteLine("3. CONSISTENT LOCK ORDERING");
            Console.WriteLine("----------------------------------------");

            object firstLock = new();
            object secondLock = new();

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskNumber = i + 1;

                tasks[i] = Task.Run(() =>
                {
                    lock (firstLock)
                    {
                        lock (secondLock)
                        {
                            Console.WriteLine(
                                $"Task {taskNumber} entered critical section.");
                        }
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine(
                "Every task uses the same resource acquisition order.");
        }

        private static async Task DemonstrateSemaphoreSlimPatternAsync()
        {
            Console.WriteLine("4. SEMAPHORESLIM ASYNC PATTERN");
            Console.WriteLine("----------------------------------------");

            using SemaphoreSlim semaphore =
                new(initialCount: 1, maxCount: 1);

            Task[] tasks =
            {
            ProcessWithSemaphoreAsync(
                "Task 1",
                semaphore),

            ProcessWithSemaphoreAsync(
                "Task 2",
                semaphore),

            ProcessWithSemaphoreAsync(
                "Task 3",
                semaphore)
        };

            await Task.WhenAll(tasks);

            Console.WriteLine(
                "SemaphoreSlim provided async mutual exclusion.");
        }

        private static async Task ProcessWithSemaphoreAsync(
            string taskName,
            SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();

            try
            {
                Console.WriteLine(
                    $"{taskName} entered critical section.");

                await Task.Delay(300);

                Console.WriteLine(
                    $"{taskName} leaving critical section.");
            }
            finally
            {
                semaphore.Release();
            }
        }

        private static void DemonstrateMonitorTimeout()
        {
            Console.WriteLine("5. MONITOR TIMEOUT");
            Console.WriteLine("----------------------------------------");

            object lockObject = new();

            Task holder = Task.Run(() =>
            {
                lock (lockObject)
                {
                    Console.WriteLine(
                        "Task 1 acquired the lock.");

                    Thread.Sleep(1500);

                    Console.WriteLine(
                        "Task 1 released the lock.");
                }
            });

            Thread.Sleep(100);

            Task waiter = Task.Run(() =>
            {
                bool lockAcquired = Monitor.TryEnter(
                    lockObject,
                    TimeSpan.FromMilliseconds(500));

                if (!lockAcquired)
                {
                    Console.WriteLine(
                        "Task 2 could not acquire the lock within the timeout.");
                    return;
                }

                try
                {
                    Console.WriteLine(
                        "Task 2 acquired the lock.");
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            });

            Task.WaitAll(
                holder,
                waiter);

            Console.WriteLine(
                "Timeout prevents indefinite waiting, but it does not replace correct synchronization design.");
        }
    }
}

    //Call it with:

    //await Deadlock.RunAsync();

    //Important: The first demonstration intentionally creates a deadlock, 
    //    but uses a Task.WhenAny(..., Task.Delay(...)) timeout so the demo itself doesn't hang forever. 
    //    The deadlocked tasks are deliberately left unawaited and use isolated locks so the rest of the examples can continue
