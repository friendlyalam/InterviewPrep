
namespace InterviewPrep.CSharp.Collections.Generics._03_HashSet
{
    public class GenericHashSetExample
    {
        public void HasSetExample()
        {

            HashSet<int> numbers = new()
{
    10,
    20,
    30,
    40
};

            Console.WriteLine("===== COUNT =====");

            Console.WriteLine($"Count: {numbers.Count}");


            Console.WriteLine("\n===== ADD =====");

            bool added = numbers.Add(50);

            Console.WriteLine($"50 added: {added}");

            bool duplicateAdded = numbers.Add(30);

            Console.WriteLine($"30 added again: {duplicateAdded}");


            Console.WriteLine("\n===== CONTAINS =====");

            Console.WriteLine(
                $"Contains 20: {numbers.Contains(20)}");

            Console.WriteLine(
                $"Contains 100: {numbers.Contains(100)}");


            Console.WriteLine("\n===== REMOVE =====");

            bool removed = numbers.Remove(20);

            Console.WriteLine(
                $"20 removed: {removed}");


            Console.WriteLine("\n===== REMOVE WHERE =====");

            numbers.RemoveWhere(number => number > 40);

            Console.WriteLine(
                $"Count after RemoveWhere: {numbers.Count}");


            Console.WriteLine("\n===== CURRENT VALUES =====");

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }


            Console.WriteLine("\n===== UNION =====");

            HashSet<int> setA = new() { 1, 2, 3, 4 };
            HashSet<int> setB = new() { 3, 4, 5, 6 };

            setA.UnionWith(setB);

            Console.WriteLine(
                $"Union: {string.Join(", ", setA)}");


            Console.WriteLine("\n===== INTERSECTION =====");

            HashSet<int> setC = new() { 1, 2, 3, 4 };
            HashSet<int> setD = new() { 3, 4, 5, 6 };

            setC.IntersectWith(setD);

            Console.WriteLine(
                $"Intersection: {string.Join(", ", setC)}");


            Console.WriteLine("\n===== DIFFERENCE =====");

            HashSet<int> setE = new() { 1, 2, 3, 4 };
            HashSet<int> setF = new() { 3, 4, 5, 6 };

            setE.ExceptWith(setF);

            Console.WriteLine(
                $"Difference: {string.Join(", ", setE)}");


            Console.WriteLine("\n===== SYMMETRIC DIFFERENCE =====");

            HashSet<int> setG = new() { 1, 2, 3, 4 };
            HashSet<int> setH = new() { 3, 4, 5, 6 };

            setG.SymmetricExceptWith(setH);

            Console.WriteLine(
                $"Symmetric difference: {string.Join(", ", setG)}");


            Console.WriteLine("\n===== SUBSET =====");

            HashSet<int> smallSet = new() { 1, 2 };
            HashSet<int> largeSet = new() { 1, 2, 3, 4 };

            Console.WriteLine(
                $"smallSet is subset: " +
                $"{smallSet.IsSubsetOf(largeSet)}");


            Console.WriteLine("\n===== SUPERSET =====");

            Console.WriteLine(
                $"largeSet is superset: " +
                $"{largeSet.IsSupersetOf(smallSet)}");


            Console.WriteLine("\n===== OVERLAPS =====");

            HashSet<int> overlapA = new() { 1, 2, 3 };
            HashSet<int> overlapB = new() { 3, 4, 5 };

            Console.WriteLine(
                $"Sets overlap: {overlapA.Overlaps(overlapB)}");


            Console.WriteLine("\n===== SET EQUALS =====");

            HashSet<int> equalsA = new() { 1, 2, 3 };
            HashSet<int> equalsB = new() { 3, 2, 1 };

            Console.WriteLine(
                $"Sets equal: {equalsA.SetEquals(equalsB)}");


            Console.WriteLine("\n===== COPY TO =====");

            int[] array = new int[equalsA.Count];

            equalsA.CopyTo(array);

            Console.WriteLine(
                $"Array: {string.Join(", ", array)}");


            Console.WriteLine("\n===== TO ARRAY =====");

            int[] convertedArray = equalsA.ToArray();

            Console.WriteLine(
                $"Array: {string.Join(", ", convertedArray)}");


            Console.WriteLine("\n===== DUPLICATE DETECTION =====");

            int[] input =
            {
    10, 20, 30, 40, 20
};

            HashSet<int> seen = new();

            foreach (int number in input)
            {
                if (!seen.Add(number))
                {
                    Console.WriteLine(
                        $"Duplicate found: {number}");

                    break;
                }
            }


            Console.WriteLine("\n===== CLEAR =====");

            numbers.Clear();

            Console.WriteLine(
                $"Count after Clear: {numbers.Count}");
        }
    }
}
