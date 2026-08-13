using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._08_SortedDictionary
{
    public class GenericSortedDictionaryExample
    {
        public void SortedDictionaryExample()
        {
            SortedDictionary<int, string> students = new();

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
                    out string? studentName))
            {
                Console.WriteLine(
                    $"Student 103: {studentName}");
            }


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


            Console.WriteLine("\n===== REMOVE =====");

            Console.WriteLine(
                $"Removed 102: {students.Remove(102)}");

            Console.WriteLine(
                $"Count: {students.Count}");


            Console.WriteLine("\n===== REMOVE WITH VALUE =====");

            students.Add(102, "Priya");

            Console.WriteLine(
                $"Removed 102/Priya: " +
                $"{students.Remove(102)}");


            Console.WriteLine("\n===== REVERSE ENUMERATION =====");

            foreach (var student in students.Reverse())
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }


            Console.WriteLine("\n===== CUSTOM DESCENDING ORDER =====");

            SortedDictionary<int, string> descending =
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
