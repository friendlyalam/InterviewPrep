using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.Collections.Generics._04_SortedSet
{
    public class GenericSortedSetExample
    {
        public void SortedSetExample() {
            SortedSet<int> scores = new()
{
    50,
    20,
    80,
    30,
    20,
    70,
    50
};

            Console.WriteLine("===== SORTED SET =====");

            foreach (int score in scores)
            {
                Console.WriteLine(score);
            }


            Console.WriteLine("\n===== COUNT =====");

            Console.WriteLine(scores.Count);


            Console.WriteLine("\n===== MIN / MAX =====");

            Console.WriteLine($"Min: {scores.Min}");
            Console.WriteLine($"Max: {scores.Max}");


            Console.WriteLine("\n===== ADD =====");

            Console.WriteLine($"90 added: {scores.Add(90)}");
            Console.WriteLine($"50 added again: {scores.Add(50)}");


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 70: {scores.Contains(70)}");

            Console.WriteLine(
                $"Contains 100: {scores.Contains(100)}");


            Console.WriteLine("\n===== REMOVE =====");

            Console.WriteLine(
                $"70 removed: {scores.Remove(70)}");


            Console.WriteLine("\n===== REMOVE WHERE =====");

            scores.RemoveWhere(x => x > 60);


            Console.WriteLine("\n===== CURRENT VALUES =====");

            foreach (int score in scores)
            {
                Console.WriteLine(score);
            }


            Console.WriteLine("\n===== REVERSE =====");

            foreach (int score in scores.Reverse())
            {
                Console.WriteLine(score);
            }


            Console.WriteLine("\n===== RANGE VIEW =====");

            SortedSet<int> allScores = new()
{
    10, 20, 30, 40, 50, 60, 70, 80
};

            var range = allScores.GetViewBetween(30, 60);

            foreach (int score in range)
            {
                Console.WriteLine(score);
            }


            Console.WriteLine("\n===== UNION =====");

            SortedSet<int> setA = new() { 1, 2, 3, 4 };
            SortedSet<int> setB = new() { 3, 4, 5, 6 };

            setA.UnionWith(setB);

            Console.WriteLine(
                string.Join(", ", setA));


            Console.WriteLine("\n===== INTERSECTION =====");

            SortedSet<int> setC = new() { 1, 2, 3, 4 };
            SortedSet<int> setD = new() { 3, 4, 5, 6 };

            setC.IntersectWith(setD);

            Console.WriteLine(
                string.Join(", ", setC));


            Console.WriteLine("\n===== DIFFERENCE =====");

            SortedSet<int> setE = new() { 1, 2, 3, 4 };
            SortedSet<int> setF = new() { 3, 4, 5, 6 };

            setE.ExceptWith(setF);

            Console.WriteLine(
                string.Join(", ", setE));


            Console.WriteLine("\n===== SUBSET =====");

            SortedSet<int> small = new() { 1, 2 };
            SortedSet<int> large = new() { 1, 2, 3, 4 };

            Console.WriteLine(
                small.IsSubsetOf(large));


            Console.WriteLine("\n===== OVERLAP =====");

            SortedSet<int> overlapA = new() { 1, 2, 3 };
            SortedSet<int> overlapB = new() { 3, 4, 5 };

            Console.WriteLine(
                overlapA.Overlaps(overlapB));


            Console.WriteLine("\n===== SET EQUALS =====");

            SortedSet<int> equalsA = new() { 1, 2, 3 };
            SortedSet<int> equalsB = new() { 3, 2, 1 };

            Console.WriteLine(
                equalsA.SetEquals(equalsB));


            Console.WriteLine("\n===== COPY TO =====");

            int[] array = new int[equalsA.Count];

            equalsA.CopyTo(array);

            Console.WriteLine(
                string.Join(", ", array));


            Console.WriteLine("\n===== CLEAR =====");

            scores.Clear();

            Console.WriteLine(
                $"Count after Clear: {scores.Count}");
        }
    }
}
