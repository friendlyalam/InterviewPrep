using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.NonGenerics._03_SortedList
{
    public class NonGenericSortedListExample
    {
        public void SortedlistExample() {
            SortedList students = new();

            students.Add(103, "Rahul");
            students.Add(101, "Aman");
            students.Add(102, "Priya");

            Console.WriteLine("===== SORTED DATA =====");

            foreach (DictionaryEntry student in students)
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }


            Console.WriteLine("\n===== LOOKUP =====");

            Console.WriteLine(
                $"Student 101: {students[101]}");


            Console.WriteLine("\n===== UPDATE =====");

            students[101] = "Arjun";

            Console.WriteLine(
                $"Student 101: {students[101]}");


            Console.WriteLine("\n===== CONTAINS KEY =====");

            Console.WriteLine(
                students.ContainsKey(102));


            Console.WriteLine("\n===== INDEX OF KEY =====");

            Console.WriteLine(
                students.IndexOfKey(102));


            Console.WriteLine("\n===== GET KEY BY INDEX =====");

            Console.WriteLine(
                students.GetKey(0));


            Console.WriteLine("\n===== GET VALUE BY INDEX =====");

            Console.WriteLine(
                students.GetByIndex(0));


            Console.WriteLine("\n===== REMOVE BY KEY =====");

            students.Remove(102);


            Console.WriteLine("\n===== FINAL DATA =====");

            foreach (DictionaryEntry student in students)
            {
                Console.WriteLine(
                    $"{student.Key} -> {student.Value}");
            }
        }
    }
}
