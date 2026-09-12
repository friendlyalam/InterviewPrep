using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._08_Synchronization
{
    public static class Synchronization_Examples
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("SYNCHRONIZATION EXAMPLES");
            Console.WriteLine("========================================");

            DemonstrateLock();

            Console.WriteLine();

            DemonstrateMonitor();

            Console.WriteLine();

            await DemonstrateSemaphoreSlimAsync();

            Console.WriteLine();

            DemonstrateMutex();

            Console.WriteLine();

            DemonstrateReaderWriterLockSlim();

            Console.WriteLine();

            Console.WriteLine("All synchronization examples completed.");
        }

        // ============================================================
        // 1. LOCK
        // ============================================================

        private static void DemonstrateLock()
        {
            Console.WriteLine("1. LOCK");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            object lockObject = new();

            Task[] tasks = new Task[5];

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

            Task.WaitAll(tasks);

            Console.WriteLine(
                $"Expected counter: {tasks.Length * 10_000}");

            Console.WriteLine(
                $"Actual counter:   {counter}");

            Console.WriteLine(
                "lock provides simple in-process mutual exclusion.");
        }

        // ============================================================
        // 2. MONITOR
        // ============================================================

        private static void DemonstrateMonitor()
        {
            Console.WriteLine("2. MONITOR");
            Console.WriteLine("----------------------------------------");

            int counter = 0;

            object syncObject = new();

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 10_000; j++)
                    {
                        Monitor.Enter(syncObject);

                        try
                        {
                            counter++;
                        }
                        finally
                        {
                            Monitor.Exit(syncObject);
                        }
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine(
                $"Expected counter: {tasks.Length * 10_000}");

            Console.WriteLine(
                $"Actual counter:   {counter}");

            Console.WriteLine(
                "Monitor provides explicit control over entering and exiting a critical section.");
        }

        // ============================================================
        // 3. SEMAPHORE SLIM
        // ============================================================

        private static async Task DemonstrateSemaphoreSlimAsync()
        {
            Console.WriteLine("3. SEMAPHORE SLIM");
            Console.WriteLine("----------------------------------------");

            using SemaphoreSlim semaphore =
                new(initialCount: 2, maxCount: 2);

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                int taskNumber = i + 1;

                tasks[i] = ProcessWithSemaphoreAsync(
                    taskNumber,
                    semaphore);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(
                "SemaphoreSlim allowed a maximum of 2 concurrent operations.");
        }

        private static async Task ProcessWithSemaphoreAsync(
            int taskNumber,
            SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();

            try
            {
                Console.WriteLine(
                    $"Task {taskNumber} entered semaphore | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");

                await Task.Delay(1000);

                Console.WriteLine(
                    $"Task {taskNumber} leaving semaphore | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");
            }
            finally
            {
                semaphore.Release();
            }
        }

        // ============================================================
        // 4. MUTEX
        // ============================================================

        private static void DemonstrateMutex()
        {
            Console.WriteLine("4. MUTEX");
            Console.WriteLine("----------------------------------------");

            using Mutex mutex = new();

            int counter = 0;

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    mutex.WaitOne();

                    try
                    {
                        for (int j = 0; j < 10_000; j++)
                        {
                            counter++;
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine(
                $"Expected counter: {tasks.Length * 10_000}");

            Console.WriteLine(
                $"Actual counter:   {counter}");

            Console.WriteLine(
                "Mutex provides mutual exclusion and can support cross-process synchronization when named.");
        }

        // ============================================================
        // 5. READER WRITER LOCK SLIM
        // ============================================================

        private static void DemonstrateReaderWriterLockSlim()
        {
            Console.WriteLine("5. READERWRITERLOCKSLIM");
            Console.WriteLine("----------------------------------------");

            using ReaderWriterLockSlim rwLock =
                new();

            int sharedValue = 100;

            Task reader1 = Task.Run(() =>
            {
                ReadValue(
                    "Reader 1",
                    rwLock,
                    ref sharedValue);
            });

            Task reader2 = Task.Run(() =>
            {
                ReadValue(
                    "Reader 2",
                    rwLock,
                    ref sharedValue);
            });

            Task reader3 = Task.Run(() =>
            {
                ReadValue(
                    "Reader 3",
                    rwLock,
                    ref sharedValue);
            });

            Task writer = Task.Run(() =>
            {
                Thread.Sleep(300);

                rwLock.EnterWriteLock();

                try
                {
                    Console.WriteLine(
                        $"Writer entered | " +
                        $"Thread: {Environment.CurrentManagedThreadId}");

                    sharedValue += 50;

                    Thread.Sleep(500);

                    Console.WriteLine(
                        $"Writer updated value to: {sharedValue}");
                }
                finally
                {
                    rwLock.ExitWriteLock();
                }
            });

            Task.WaitAll(
                reader1,
                reader2,
                reader3,
                writer);

            Console.WriteLine(
                $"Final shared value: {sharedValue}");

            Console.WriteLine(
                "ReaderWriterLockSlim allows concurrent readers but exclusive writers.");
        }

        private static void ReadValue(
            string readerName,
            ReaderWriterLockSlim rwLock,
            ref int sharedValue)
        {
            rwLock.EnterReadLock();

            try
            {
                Console.WriteLine(
                    $"{readerName} entered | " +
                    $"Value: {sharedValue} | " +
                    $"Thread: {Environment.CurrentManagedThreadId}");

                Thread.Sleep(500);

                Console.WriteLine(
                    $"{readerName} completed.");
            }
            finally
            {
                rwLock.ExitReadLock();
            }
        }
    }

    // program.cs file
    //await Synchronization_Examples.RunAsync();
}
