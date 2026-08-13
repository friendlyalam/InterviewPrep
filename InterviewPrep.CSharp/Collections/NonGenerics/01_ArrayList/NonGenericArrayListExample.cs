using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.NonGenerics._01_ArrayList
{
    public class NonGenericArrayListExample
    {
        public void ArrayListExample() {
            ArrayList numbers = new();

            Console.WriteLine("===== ADD =====");

            numbers.Add(10);
            numbers.Add(20);
            numbers.Add(30);

            foreach (object number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== ADD RANGE =====");

            numbers.AddRange(new int[] { 40, 50 });

            foreach (object number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== INSERT =====");

            numbers.Insert(1, 99);

            foreach (object number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 30: {numbers.Contains(30)}");


            Console.WriteLine("\n===== INDEX OF =====");

            Console.WriteLine(
                $"Index of 30: {numbers.IndexOf(30)}");


            Console.WriteLine("\n===== ACCESS =====");

            int firstNumber = (int)numbers[0];

            Console.WriteLine(
                $"First number: {firstNumber}");


            Console.WriteLine("\n===== COUNT =====");

            Console.WriteLine(
                $"Count: {numbers.Count}");


            Console.WriteLine("\n===== CAPACITY =====");

            Console.WriteLine(
                $"Capacity: {numbers.Capacity}");


            Console.WriteLine("\n===== REMOVE =====");

            numbers.Remove(99);

            Console.WriteLine(
                $"Count after Remove: {numbers.Count}");


            Console.WriteLine("\n===== REMOVE AT =====");

            numbers.RemoveAt(0);

            foreach (object number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== CLEAR =====");

            numbers.Clear();

            Console.WriteLine(
                $"Count after Clear: {numbers.Count}");
        }
    }
}
