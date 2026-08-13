using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._05_Stack
{
    public class GenericStackExample
    {
        public void StackExample() {
            Stack<int> numbers = new();

            Console.WriteLine("===== PUSH =====");

            numbers.Push(10);
            numbers.Push(20);
            numbers.Push(30);

            Console.WriteLine($"Count: {numbers.Count}");


            Console.WriteLine("\n===== PEEK =====");

            Console.WriteLine(
                $"Top element: {numbers.Peek()}");

            Console.WriteLine(
                $"Count after Peek: {numbers.Count}");


            Console.WriteLine("\n===== POP =====");

            int removed = numbers.Pop();

            Console.WriteLine(
                $"Removed: {removed}");

            Console.WriteLine(
                $"Count after Pop: {numbers.Count}");


            Console.WriteLine("\n===== TRY PEEK =====");

            if (numbers.TryPeek(out int top))
            {
                Console.WriteLine(
                    $"Top element: {top}");
            }


            Console.WriteLine("\n===== TRY POP =====");

            if (numbers.TryPop(out int value))
            {
                Console.WriteLine(
                    $"Removed: {value}");
            }


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 10: {numbers.Contains(10)}");

            Console.WriteLine(
                $"Contains 100: {numbers.Contains(100)}");


            Console.WriteLine("\n===== CURRENT STACK =====");

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== TO ARRAY =====");

            int[] array = numbers.ToArray();

            Console.WriteLine(
                string.Join(", ", array));


            Console.WriteLine("\n===== COPY TO =====");

            int[] copiedArray = new int[numbers.Count];

            numbers.CopyTo(copiedArray, 0);

            Console.WriteLine(
                string.Join(", ", copiedArray));


            Console.WriteLine("\n===== ENSURE CAPACITY =====");

            numbers.EnsureCapacity(100);

            Console.WriteLine(
                "Capacity requirement ensured.");


            Console.WriteLine("\n===== CLEAR =====");

            numbers.Clear();

            Console.WriteLine(
                $"Count after Clear: {numbers.Count}");


            Console.WriteLine("\n===== TRY POP ON EMPTY STACK =====");

            if (!numbers.TryPop(out int emptyValue))
            {
                Console.WriteLine(
                    "Stack is empty.");
            }
        }
    }
}
