using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.NonGenerics._02_Hashtable
{
    public class NonGenericHashtable
    {
        public void HashtableExample() {
            Hashtable employees = new();

            Console.WriteLine("===== ADD =====");

            employees.Add(103, "Rahul");
            employees.Add(101, "Aman");
            employees.Add(102, "Priya");

            foreach (DictionaryEntry employee in employees)
            {
                Console.WriteLine(
                    $"{employee.Key} -> {employee.Value}");
            }


            Console.WriteLine("\n===== LOOKUP =====");

            string name =
                (string)employees[101];

            Console.WriteLine(
                $"Employee 101: {name}");


            Console.WriteLine("\n===== UPDATE =====");

            employees[101] = "Arjun";

            Console.WriteLine(
                $"Employee 101: {employees[101]}");


            Console.WriteLine("\n===== CONTAINS KEY =====");

            Console.WriteLine(
                $"Contains 102: " +
                $"{employees.ContainsKey(102)}");


            Console.WriteLine("\n===== CONTAINS VALUE =====");

            Console.WriteLine(
                $"Contains Priya: " +
                $"{employees.ContainsValue("Priya")}");


            Console.WriteLine("\n===== KEYS =====");

            foreach (object key in employees.Keys)
            {
                Console.WriteLine(key);
            }


            Console.WriteLine("\n===== VALUES =====");

            foreach (object value in employees.Values)
            {
                Console.WriteLine(value);
            }


            Console.WriteLine("\n===== COUNT =====");

            Console.WriteLine(
                $"Count: {employees.Count}");


            Console.WriteLine("\n===== REMOVE =====");

            employees.Remove(102);

            Console.WriteLine(
                $"Count after Remove: " +
                $"{employees.Count}");


            Console.WriteLine("\n===== CLEAR =====");

            employees.Clear();

            Console.WriteLine(
                $"Count after Clear: " +
                $"{employees.Count}");
        
        }
    }
}
