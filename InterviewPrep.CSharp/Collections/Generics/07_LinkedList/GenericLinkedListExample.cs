using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._07_LinkedList
{
    public class GenericLinkedListExample
    {
        public void LinkedListExample() {
            LinkedList<int> numbers = new();

            Console.WriteLine("===== ADD FIRST / ADD LAST =====");

            numbers.AddFirst(20);
            numbers.AddLast(40);

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== ADD BEFORE =====");

            LinkedListNode<int> node40 = numbers.Find(40)!;

            numbers.AddBefore(node40, 30);

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== ADD AFTER =====");

            numbers.AddAfter(node40, 50);

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== ADD FIRST AGAIN =====");

            numbers.AddFirst(10);

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== FIRST / LAST =====");

            Console.WriteLine(
                $"First: {numbers.First?.Value}");

            Console.WriteLine(
                $"Last: {numbers.Last?.Value}");

            Console.WriteLine(
                $"Count: {numbers.Count}");


            Console.WriteLine("\n===== FIND =====");

            LinkedListNode<int>? node30 = numbers.Find(30);

            if (node30 != null)
            {
                Console.WriteLine(
                    $"Found: {node30.Value}");
            }


            Console.WriteLine("\n===== NODE RELATIONSHIPS =====");

            if (node30 != null)
            {
                Console.WriteLine(
                    $"Previous: {node30.Previous?.Value}");

                Console.WriteLine(
                    $"Current: {node30.Value}");

                Console.WriteLine(
                    $"Next: {node30.Next?.Value}");
            }


            Console.WriteLine("\n===== FIND LAST =====");

            numbers.AddLast(30);

            LinkedListNode<int>? last30 = numbers.FindLast(30);

            if (last30 != null)
            {
                Console.WriteLine(
                    $"Last 30 found: {last30.Value}");
            }


            Console.WriteLine("\n===== REMOVE VALUE =====");

            numbers.Remove(30);

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== REMOVE NODE =====");

            if (node40 != null)
            {
                numbers.Remove(node40);
            }

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== REMOVE FIRST =====");

            numbers.RemoveFirst();

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== REMOVE LAST =====");

            numbers.RemoveLast();

            Console.WriteLine(
                string.Join(" <-> ", numbers));


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 20: {numbers.Contains(20)}");

            Console.WriteLine(
                $"Contains 100: {numbers.Contains(100)}");


            Console.WriteLine("\n===== FORWARD TRAVERSAL =====");

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== BACKWARD TRAVERSAL =====");

            LinkedListNode<int>? current = numbers.Last;

            while (current != null)
            {
                Console.WriteLine(current.Value);

                current = current.Previous;
            }


            Console.WriteLine("\n===== COPY TO =====");

            int[] array = new int[numbers.Count];

            numbers.CopyTo(array, 0);

            Console.WriteLine(
                string.Join(", ", array));


            Console.WriteLine("\n===== CLEAR =====");

            numbers.Clear();

            Console.WriteLine(
                $"Count after Clear: {numbers.Count}");
        }
    }
}
