using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._01_List
{
    public class GenericListExample
    {
        public void GenericListBuiltMethods()
        {
            List<int> numbers = new()
{
    10,
    20,
    30,
    40,
    50
};

            Console.WriteLine("===== PROPERTIES =====");

            Console.WriteLine($"Count: {numbers.Count}");
            Console.WriteLine($"Capacity: {numbers.Capacity}");


            Console.WriteLine("\n===== ADD =====");

            numbers.Add(60);

            Console.WriteLine(
                $"After Add(60): {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== ADD RANGE =====");

            numbers.AddRange(new[] { 70, 80, 90 });

            Console.WriteLine(
                $"After AddRange: {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== INSERT =====");

            numbers.Insert(1, 15);

            Console.WriteLine(
                $"After Insert(1, 15): {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== INSERT RANGE =====");

            numbers.InsertRange(2, new[] { 17, 18 });

            Console.WriteLine(
                $"After InsertRange: {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== INDEX ACCESS =====");

            Console.WriteLine($"Element at index 0: {numbers[0]}");

            numbers[0] = 5;

            Console.WriteLine(
                $"After updating index 0: {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 30: {numbers.Contains(30)}");

            Console.WriteLine(
                $"Contains 100: {numbers.Contains(100)}");


            Console.WriteLine("\n===== INDEX OF =====");

            Console.WriteLine(
                $"IndexOf(30): {numbers.IndexOf(30)}");


            Console.WriteLine("\n===== LAST INDEX OF =====");

            numbers.Add(30);

            Console.WriteLine(
                $"LastIndexOf(30): {numbers.LastIndexOf(30)}");


            Console.WriteLine("\n===== FIND =====");

            int firstGreaterThan30 =
                numbers.Find(number => number > 30);

            Console.WriteLine(
                $"First number > 30: {firstGreaterThan30}");


            Console.WriteLine("\n===== FIND LAST =====");

            int lastGreaterThan30 =
                numbers.FindLast(number => number > 30);

            Console.WriteLine(
                $"Last number > 30: {lastGreaterThan30}");


            Console.WriteLine("\n===== FIND ALL =====");

            List<int> greaterThan30 =
                numbers.FindAll(number => number > 30);

            Console.WriteLine(
                $"Numbers > 30: {string.Join(", ", greaterThan30)}");


            Console.WriteLine("\n===== EXISTS =====");

            bool existsGreaterThan50 =
                numbers.Exists(number => number > 50);

            Console.WriteLine(
                $"Exists number > 50: {existsGreaterThan50}");


            Console.WriteLine("\n===== TRUE FOR ALL =====");

            bool allPositive =
                numbers.TrueForAll(number => number > 0);

            Console.WriteLine(
                $"All numbers positive: {allPositive}");


            Console.WriteLine("\n===== REMOVE =====");

            numbers.Remove(30);

            Console.WriteLine(
                $"After Remove(30): {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== REMOVE AT =====");

            numbers.RemoveAt(0);

            Console.WriteLine(
                $"After RemoveAt(0): {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== REMOVE RANGE =====");

            numbers.RemoveRange(0, 2);

            Console.WriteLine(
                $"After RemoveRange(0, 2): {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== REMOVE ALL =====");

            numbers.RemoveAll(number => number > 50);

            Console.WriteLine(
                $"After RemoveAll(number > 50): {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== SORT =====");

            numbers.Sort();

            Console.WriteLine(
                $"After Sort: {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== REVERSE =====");

            numbers.Reverse();

            Console.WriteLine(
                $"After Reverse: {string.Join(", ", numbers)}");


            Console.WriteLine("\n===== FOREACH =====");

            numbers.ForEach(number =>
            {
                Console.WriteLine($"Number: {number}");
            });


            Console.WriteLine("\n===== TO ARRAY =====");

            int[] array = numbers.ToArray();

            Console.WriteLine(
                $"Array: {string.Join(", ", array)}");


            Console.WriteLine("\n===== GET RANGE =====");

            if (numbers.Count >= 2)
            {
                List<int> range =
                    numbers.GetRange(0, 2);

                Console.WriteLine(
                    $"GetRange(0, 2): {string.Join(", ", range)}");
            }


            Console.WriteLine("\n===== COPY TO =====");

            int[] copiedArray = new int[numbers.Count];

            numbers.CopyTo(copiedArray);

            Console.WriteLine(
                $"Copied array: {string.Join(", ", copiedArray)}");


            Console.WriteLine("\n===== BINARY SEARCH =====");

            numbers.Sort();

            int index =
                numbers.BinarySearch(30);

            Console.WriteLine(
                $"BinarySearch(30): {index}");


            Console.WriteLine("\n===== CLEAR =====");

            numbers.Clear();

            Console.WriteLine($"Count after Clear: {numbers.Count}");
            Console.WriteLine($"Capacity after Clear: {numbers.Capacity}");
        }
    }
}
