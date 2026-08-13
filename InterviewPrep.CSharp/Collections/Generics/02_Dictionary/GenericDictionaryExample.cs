
namespace InterviewPrep.CSharp.Collections.Generics._02_Dictionary
{
    public class GenericDictionaryExample
    {
        public void DictionaryExample()
        {
            Dictionary<int, string> employees = new()
{
    { 101, "Ali" },
    { 102, "Ahmed" },
    { 103, "John" }
};

            Console.WriteLine("===== COUNT =====");

            Console.WriteLine($"Employee count: {employees.Count}");


            Console.WriteLine("\n===== ADD =====");

            employees.Add(104, "David");

            Console.WriteLine(
                $"Employee 104: {employees[104]}");


            Console.WriteLine("\n===== TRY ADD =====");

            bool added = employees.TryAdd(105, "Sara");

            Console.WriteLine(
                $"Was employee 105 added? {added}");


            bool duplicateAdded =
                employees.TryAdd(101, "New Ali");

            Console.WriteLine(
                $"Was employee 101 added again? {duplicateAdded}");


            Console.WriteLine("\n===== INDEXER =====");

            Console.WriteLine(
                $"Employee 101: {employees[101]}");

            employees[101] = "Mohammad Ali";

            Console.WriteLine(
                $"Updated employee 101: {employees[101]}");


            Console.WriteLine("\n===== CONTAINS KEY =====");

            Console.WriteLine(
                $"Contains key 102: {employees.ContainsKey(102)}");

            Console.WriteLine(
                $"Contains key 999: {employees.ContainsKey(999)}");


            Console.WriteLine("\n===== CONTAINS VALUE =====");

            Console.WriteLine(
                $"Contains value Ahmed: {employees.ContainsValue("Ahmed")}");


            Console.WriteLine("\n===== TRY GET VALUE =====");

            if (employees.TryGetValue(103, out string? employeeName))
            {
                Console.WriteLine(
                    $"Employee 103: {employeeName}");
            }

            if (!employees.TryGetValue(999, out string? missingEmployee))
            {
                Console.WriteLine(
                    "Employee 999 was not found.");
            }


            Console.WriteLine("\n===== GET VALUE OR DEFAULT =====");

            string? name =
                employees.GetValueOrDefault(102);

            Console.WriteLine(
                $"Employee 102: {name}");


            Console.WriteLine("\n===== KEYS =====");

            foreach (int key in employees.Keys)
            {
                Console.WriteLine($"Key: {key}");
            }


            Console.WriteLine("\n===== VALUES =====");

            foreach (string value in employees.Values)
            {
                Console.WriteLine($"Value: {value}");
            }


            Console.WriteLine("\n===== KEY-VALUE PAIRS =====");

            foreach (KeyValuePair<int, string> employee in employees)
            {
                Console.WriteLine(
                    $"{employee.Key} → {employee.Value}");
            }


            Console.WriteLine("\n===== REMOVE =====");

            bool removed =
                employees.Remove(104);

            Console.WriteLine(
                $"Was employee 104 removed? {removed}");


            Console.WriteLine("\n===== REMOVE WITH VALUE =====");

            if (employees.Remove(
                    105,
                    out string? removedEmployee))
            {
                Console.WriteLine(
                    $"Removed employee: {removedEmployee}");
            }


            Console.WriteLine("\n===== ENSURE CAPACITY =====");

            int capacity =
                employees.EnsureCapacity(100);

            Console.WriteLine(
                $"Capacity ensured: {capacity}");


            Console.WriteLine("\n===== CLEAR =====");

            employees.Clear();

            Console.WriteLine(
                $"Count after Clear: {employees.Count}");
        }
    }
}
