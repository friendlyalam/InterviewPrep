using InterviewPrep.DSA.Strings;

namespace InterviewPrep.Tests.Strings
{
    public class ValidAnagramTests
    {
        private readonly ValidAnagram _solver = new ValidAnagram();

        [Theory]
        // 🧪 Test Case 1 (Basic Anagram)
        [InlineData("anagram", "nagaram", true)]

        // 🧪 Test Case 2 (Not Anagram)
        [InlineData("rat", "car", false)]

        // 🧪 Test Case 3 (Duplicate Characters)
        [InlineData("aab", "aba", true)]

        // 🧪 Test Case 4 (Different Frequency)
        [InlineData("aab", "abb", false)]

        // 🧪 Test Case 5 (Empty Strings)
        [InlineData("", "", true)]

        // 🧪 Test Case 6 (Single Character)
        [InlineData("a", "a", true)]

        // 🧪 Test Case 7 (Case Sensitive)
        [InlineData("AnaGram", "graMAna", false)] // Returns false because 'A' != 'a'

        // 🧪 Test Case 9 (Special Characters)
        [InlineData("a!b@c", "c@b!a", true)]

        // 🧪 Test Case 10 (Different Length)
        [InlineData("abc", "ab", false)]
        public void Test_Anagram_Dictionary_Scenarios(string s, string t, bool expectedResult)
        {
            // Act
            bool result = _solver.IsAnagramByDictionary(s, t);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        // 🧪 Test Case 8 (Case-Insensitive Version)
        [Fact]
        public void Test_Case_Insensitive_Anagram_With_Normalization()
        {
            // Input values
            string s = "AnaGram";
            string t = "graMAna";
            bool expectedResult = true;

            // Normalize both inputs to lowercase before passing
            string normalizedS = s.ToLower();
            string normalizedT = t.ToLower();

            // Act
            bool result = _solver.IsAnagramByDictionary(normalizedS, normalizedT);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
