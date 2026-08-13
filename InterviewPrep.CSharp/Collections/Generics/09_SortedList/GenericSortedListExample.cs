using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._09_SortedList
{
    public class GenericSortedListExample
    {
        public void SortedListExample()
        {
            SortedList<int, string> students = new();

            Console.WriteLine("===== ADD =====");

            students.Add(103, "Rahul");
            students.Add(101, "Aman");
            students.Add(102, "Priya");

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }


            Console.WriteLine("\n===== INDEXER =====");

            Console.WriteLine(
                $"Student 101: {students[101]}");


            Console.WriteLine("\n===== UPDATE =====");

            students[101] = "Arjun";

            Console.WriteLine(
                $"Student 101: {students[101]}");


            Console.WriteLine("\n===== ADD USING INDEXER =====");

            students[104] = "Sara";

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }


            Console.WriteLine("\n===== CONTAINS KEY =====");

            Console.WriteLine(
                $"Contains 102: {students.ContainsKey(102)}");

            Console.WriteLine(
                $"Contains 999: {students.ContainsKey(999)}");


            Console.WriteLine("\n===== CONTAINS VALUE =====");

            Console.WriteLine(
                $"Contains Priya: " +
                $"{students.ContainsValue("Priya")}");


            Console.WriteLine("\n===== TRY GET VALUE =====");

            if (students.TryGetValue(
                    103,
                    out string? name))
            {
                Console.WriteLine(
                    $"Student 103: {name}");
            }


            Console.WriteLine("\n===== INDEX OF KEY =====");

            int keyIndex =
                students.IndexOfKey(102);

            Console.WriteLine(
                $"Index of key 102: {keyIndex}");


            Console.WriteLine("\n===== INDEX OF VALUE =====");

            int valueIndex =
                students.IndexOfValue("Priya");

            Console.WriteLine(
                $"Index of Priya: {valueIndex}");


            Console.WriteLine("\n===== GET KEY AT INDEX =====");

            int keyAtIndex =
                students.GetKeyAtIndex(0);

            Console.WriteLine(
                $"Key at index 0: {keyAtIndex}");


            Console.WriteLine("\n===== GET VALUE AT INDEX =====");

            string valueAtIndex =
                students.GetValueAtIndex(0);

            Console.WriteLine(
                $"Value at index 0: {valueAtIndex}");


            Console.WriteLine("\n===== SET VALUE AT INDEX =====");

            students.SetValueAtIndex(
                0,
                "Updated Student");

            Console.WriteLine(
                $"Value at index 0: " +
                $"{students.GetValueAtIndex(0)}");


            Console.WriteLine("\n===== KEYS =====");

            foreach (int key in students.Keys)
            {
                Console.WriteLine(key);
            }


            Console.WriteLine("\n===== VALUES =====");

            foreach (string value in students.Values)
            {
                Console.WriteLine(value);
            }


            Console.WriteLine("\n===== COUNT =====");

            Console.WriteLine(
                $"Count: {students.Count}");


            Console.WriteLine("\n===== CAPACITY =====");

            Console.WriteLine(
                $"Capacity: {students.Capacity}");


            Console.WriteLine("\n===== REMOVE BY KEY =====");

            Console.WriteLine(
                $"Removed 102: {students.Remove(102)}");


            Console.WriteLine("\n===== REMOVE AT INDEX =====");

            if (students.Count > 0)
            {
                students.RemoveAt(0);
            }

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }


            Console.WriteLine("\n===== TRIM EXCESS =====");

            students.TrimExcess();

            Console.WriteLine(
                $"Capacity after TrimExcess: " +
                $"{students.Capacity}");


            Console.WriteLine("\n===== CUSTOM DESCENDING ORDER =====");

            SortedList<int, string> descending =
                new(
                    Comparer<int>.Create(
                        (x, y) => y.CompareTo(x)));

            descending.Add(101, "Aman");
            descending.Add(103, "Rahul");
            descending.Add(102, "Priya");

            foreach (var student in descending)
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }


            Console.WriteLine("\n===== CLEAR =====");

            students.Clear();

            Console.WriteLine(
                $"Count after Clear: {students.Count}");
        }
    }
}