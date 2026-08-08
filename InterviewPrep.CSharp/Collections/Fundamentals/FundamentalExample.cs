
namespace InterviewPrep.CSharp.Collections.Fundamentals
{
    public class FundamentalExample
    {
        public void FundamentalEmployees()
        {
           List<string> names= new List<string>();
            names.Add("Ali");
            names.Add("Ahmad");
            names.Add("John");
            Console.WriteLine($"Employee counts are {names.Count}");

            foreach (string name in names) { 
                Console.WriteLine(name);
            }
        }
        
    }
}
