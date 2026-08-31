using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._10_PriorityQueue
{
    public class PriorityQueueExample
    {
        public void PQMethod()
        {
            PriorityQueue<string, int> queue = new();

            queue.Enqueue("Task A", 3);
            queue.Enqueue("Task B", 1);
            queue.Enqueue("Task C", 5);
            queue.Enqueue("Task D", 2);

            Console.WriteLine($"Next: {queue.Peek()}");

            while (queue.Count > 0)
            {
                string task = queue.Dequeue();

                Console.WriteLine(task);
            }
        }
    }
}
