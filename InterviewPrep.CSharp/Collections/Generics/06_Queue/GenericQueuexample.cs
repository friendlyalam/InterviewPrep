
namespace InterviewPrep.CSharp.Collections.Generics._06_Queue
{
    public class GenericQueueExample
    {
        public void QueueExample()
        {
            Queue<string> customers = new();

            Console.WriteLine("===== ENQUEUE =====");

            customers.Enqueue("Customer A");
            customers.Enqueue("Customer B");
            customers.Enqueue("Customer C");

            Console.WriteLine($"Count: {customers.Count}");


            Console.WriteLine("\n===== PEEK =====");

            Console.WriteLine(
                $"Front customer: {customers.Peek()}");

            Console.WriteLine(
                $"Count after Peek: {customers.Count}");


            Console.WriteLine("\n===== DEQUEUE =====");

            string customer = customers.Dequeue();

            Console.WriteLine(
                $"Processed: {customer}");

            Console.WriteLine(
                $"Count after Dequeue: {customers.Count}");


            Console.WriteLine("\n===== TRY PEEK =====");

            if (customers.TryPeek(out string? front))
            {
                Console.WriteLine(
                    $"Front customer: {front}");
            }


            Console.WriteLine("\n===== TRY DEQUEUE =====");

            if (customers.TryDequeue(out string? nextCustomer))
            {
                Console.WriteLine(
                    $"Processed: {nextCustomer}");
            }


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains Customer C: " +
                customers.Contains("Customer C"));

            Console.WriteLine(
                $"Contains Customer X: " +
                customers.Contains("Customer X"));


            Console.WriteLine("\n===== CURRENT QUEUE =====");

            foreach (string item in customers)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("\n===== TO ARRAY =====");

            string[] array = customers.ToArray();

            Console.WriteLine(
                string.Join(", ", array));


            Console.WriteLine("\n===== COPY TO =====");

            string[] copiedArray = new string[customers.Count];

            customers.CopyTo(copiedArray, 0);

            Console.WriteLine(
                string.Join(", ", copiedArray));


            Console.WriteLine("\n===== ENSURE CAPACITY =====");

            customers.EnsureCapacity(100);

            Console.WriteLine(
                "Capacity requirement ensured.");


            Console.WriteLine("\n===== CLEAR =====");

            customers.Clear();

            Console.WriteLine(
                $"Count after Clear: {customers.Count}");


            Console.WriteLine("\n===== TRY DEQUEUE ON EMPTY QUEUE =====");

            if (!customers.TryDequeue(out string? emptyCustomer))
            {
                Console.WriteLine(
                    "Queue is empty.");
            }

        }
    }
}
