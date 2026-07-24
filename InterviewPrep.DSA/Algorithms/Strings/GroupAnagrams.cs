using System.Text;

namespace InterviewPrep.DSA.Algorithms.Strings
{
    public class GroupAnagrams
    {
        /// <summary>
        /// Groups an array of strings into sub-lists of anagrams using a frequency-based serialization technique.
        /// <para>Time Complexity: O(N * M) where N is the number of strings in the array and M is the maximum length of a string. Generating the fixed-size 26-element key takes O(1) constant time per word.</para>
        /// <para>Space Complexity: O(N * M) to store the groups and unique serialized keys in the underlying dictionary mapping.</para>
        /// </summary>
        /// <param name="strs">An array of strings to be grouped by their anagram patterns.</param>
        /// <returns>A collection of lists, where each sub-list contains words that are anagrams of each other.</returns>
        /// <summary>
        public IList<IList<string>> GroupAnagram(string[] strs)
        {
            // Dictionary to store grouped anagrams
            // Key = frequency-based string
            // Value = list of anagrams
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

            if (strs == null || strs.Length == 0)
            {
                return new List<IList<string>>();
            }

            foreach (string word in strs)
            {
                // Step 1: Create frequency array (size 26 for a-z)
                int[] count = new int[26];

                foreach (char c in word.ToLower())
                {
                    count[c - 'a']++;   // increment frequency
                }

                // Step 2: Convert frequency array into unique string key
                // Example: "#1#0#0#1..." ensures uniqueness
                StringBuilder keyBuilder = new StringBuilder();

                for (int i = 0; i < 26; i++)
                {
                    keyBuilder.Append('#');
                    keyBuilder.Append(count[i]);
                }

                string key = keyBuilder.ToString();

                // Step 3: Add word to dictionary
                if (!map.ContainsKey(key))
                {
                    map[key] = new List<string>();
                }

                map[key].Add(word);
            }

            // Return grouped values
            return new List<IList<string>>(map.Values);
        }
    }
}
