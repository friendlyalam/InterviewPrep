namespace InterviewPrep.CSharp.MultiThreading._01_Thread_Basics
{
    public static class Thread_Basics
    {
        public static void Run()
        {
            Console.WriteLine(
                $"Main thread ID: {Thread.CurrentThread.ManagedThreadId}");

            Thread worker = new Thread(DoWork);

            Console.WriteLine($"Before Start: {worker.ThreadState}");

            worker.Start();

            Console.WriteLine($"After Start: {worker.ThreadState}");

            worker.Join();

            Console.WriteLine("Worker has completed.");
        }

        private static void DoWork()
        {
            Console.WriteLine(
                $"Worker thread ID: {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1000);

            Console.WriteLine("Worker work completed.");
        }
    }
}
