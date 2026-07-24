
using InterviewPrep.DSA.Algorithms.Strings;

namespace InterviewPrep.Tests.Strings
{
    public class GroupAnagramsTests
    {
        private readonly GroupAnagrams _solver = new GroupAnagrams();

        [Fact]
        public void Test_GroupAnagrams_All_Possible_Scenarios()
        {
            // 🧪 Scenario 1: Standard LeetCode Scenario (Mixed Anagram Groups)
            string[] scenario1Input = { "eat", "tea", "tan", "ate", "nat", "bat" };
            var scenario1Result = _solver.GroupAnagram(scenario1Input);

            Assert.Equal(3, scenario1Result.Count);
            Assert.Contains(scenario1Result, g => g.Count == 3 && g.Contains("eat") && g.Contains("tea") && g.Contains("ate"));
            Assert.Contains(scenario1Result, g => g.Count == 2 && g.Contains("tan") && g.Contains("nat"));
            Assert.Contains(scenario1Result, g => g.Count == 1 && g.Contains("bat"));

            // 🧪 Scenario 2: Empty Array
            string[] scenario2Input = System.Array.Empty<string>();
            var scenario2Result = _solver.GroupAnagram(scenario2Input);
            Assert.Empty(scenario2Result);

            // 🧪 Scenario 3: Array with a Single Empty String
            string[] scenario3Input = { "" };
            var scenario3Result = _solver.GroupAnagram(scenario3Input);
            Assert.Single(scenario3Result);
            Assert.Equal("", scenario3Result[0][0]);

            // 🧪 Scenario 4: Single Character Strings
            string[] scenario4Input = { "a" };
            var scenario4Result = _solver.GroupAnagram(scenario4Input);
            Assert.Single(scenario4Result);
            Assert.Equal("a", scenario4Result[0][0]);

            // 🧪 Scenario 5: Case-Insensitive Grouping (Ensures .ToLower() functions correctly)
            string[] scenario5Input = { "Eat", "TEA", "ate" };
            var scenario5Result = _solver.GroupAnagram(scenario5Input);
            // Since all three match the same frequency pattern natively due to .ToLower(), they should form 1 single group
            Assert.Single(scenario5Result);
            Assert.Equal(3, scenario5Result[0].Count);

            // 🧪 Scenario 6: Pure Duplicates (Identical Words)
            string[] scenario6Input = { "test", "test", "test" };
            var scenario6Result = _solver.GroupAnagram(scenario6Input);
            Assert.Single(scenario6Result);
            Assert.Equal(3, scenario6Result[0].Count);

            // 🧪 Scenario 7: Words with Duplicate Characters within themselves (Different Frequencies)
            // "aab" and "abb" have the same characters but different frequencies, so they must be split
            string[] scenario7Input = { "aab", "abb", "baa" };
            var scenario7Result = _solver.GroupAnagram(scenario7Input);
            Assert.Equal(2, scenario7Result.Count);
            Assert.Contains(scenario7Result, g => g.Count == 2 && g.Contains("aab") && g.Contains("baa"));
            Assert.Contains(scenario7Result, g => g.Count == 1 && g.Contains("abb"));

            // 🧪 Scenario 8: No Anagrams Present At All
            string[] scenario8Input = { "abc", "def", "xyz" };
            var scenario8Result = _solver.GroupAnagram(scenario8Input);
            Assert.Equal(3, scenario8Result.Count);
            foreach (var group in scenario8Result)
            {
                Assert.Single(group);
            }
        }
    }
}
