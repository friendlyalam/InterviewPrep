using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.NonGenerics._05_Queue
{
    public class NonGenericQueueExample
    {
        public void QueueExample() {
            Queue queue = new();

            Console.WriteLine("===== ENQUEUE =====");

            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);

            Console.WriteLine(
                $"Count: {queue.Count}");


            Console.WriteLine("\n===== PEEK =====");

            int front = (int)queue.Peek();

            Console.WriteLine(
                $"Front: {front}");

            Console.WriteLine(
                $"Count after Peek: {queue.Count}");


            Console.WriteLine("\n===== DEQUEUE =====");

            int removed = (int)queue.Dequeue();

            Console.WriteLine(
                $"Removed: {removed}");

            Console.WriteLine(
                $"Count after Dequeue: {queue.Count}");


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 20: {queue.Contains(20)}");


            Console.WriteLine("\n===== REMAINING =====");

            foreach (object item in queue)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("\n===== CLEAR =====");

            queue.Clear();

            Console.WriteLine(
                $"Count: {queue.Count}");
        }
    }
}
