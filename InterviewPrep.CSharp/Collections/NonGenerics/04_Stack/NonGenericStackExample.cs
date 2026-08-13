using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.NonGenerics._04_Stack
{
    public class NonGenericStackExample
    {
        public void StackExample() {
            Stack stack = new();

            Console.WriteLine("===== PUSH =====");

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            Console.WriteLine(
                $"Count: {stack.Count}");


            Console.WriteLine("\n===== PEEK =====");

            int top = (int)stack.Peek();

            Console.WriteLine(
                $"Top: {top}");

            Console.WriteLine(
                $"Count after Peek: {stack.Count}");


            Console.WriteLine("\n===== POP =====");

            int removed = (int)stack.Pop();

            Console.WriteLine(
                $"Removed: {removed}");

            Console.WriteLine(
                $"Count after Pop: {stack.Count}");


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 20: {stack.Contains(20)}");


            Console.WriteLine("\n===== REMAINING =====");

            foreach (object item in stack)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("\n===== CLEAR =====");

            stack.Clear();

            Console.WriteLine(
                $"Count: {stack.Count}");
        }
    }
}
