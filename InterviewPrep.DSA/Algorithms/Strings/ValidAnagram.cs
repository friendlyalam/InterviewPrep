namespace InterviewPrep.DSA.Algorithms.Strings
{
    public class ValidAnagram
    {
        /// <summary>
        /// Determines if two strings are anagrams of each other. 
        /// Accepts both lowercase and uppercase inputs (ensure inputs are normalized to lowercase before passing to match the 26-letter index logic).
        /// <para>Time Complexity: O(N) where N is the length of the strings.</para>
        /// <para>Space Complexity: O(1) as it uses a fixed-size array of 26 elements regardless of input size.</para>
        /// </summary>
        public bool IsAnagram(string s, string t)
        {
            // Step 1: If lengths differ → cannot be anagram
            if (s.Length != t.Length)
                return false;

            // Step 2: Create array to store frequency of 26 lowercase letters
            int[] count = new int[26]; // index 0 = 'a', 1 = 'b', ..., 25 = 'z'

            // Step 3: Count characters from first string
            foreach (char c in s)
            {
                // c - 'a' converts character to index (e.g., 'a'→0, 'b'→1)
                // count[...]++ means "increase frequency of this character"
                count[c - 'a']++;
            }

            // Step 4: Subtract using second string
            foreach (char c in t)
            {
                // Same index logic
                // count[...]-- means "remove/match frequency of this character"
                count[c - 'a']--;
            }

            // Step 5: Check if all counts are zero
            foreach (int val in count)
            {
                // If any value is not zero → mismatch → not anagram
                if (val != 0)
                    return false;
            }

            // All matched perfectly
            return true;
        }

        /// <summary>
        /// Determines if two strings are anagrams of each other using a dictionary. 
        /// Flexibly handles all character types, including both lowercase and uppercase inputs.
        /// <para>Time Complexity: O(N) where N is the length of the strings, due to single-pass iterations and O(1) dictionary lookups.</para>
        /// <para>Space Complexity: O(K) where K is the number of unique characters in the input strings stored in the dictionary.</para>
        /// </summary>
        public bool IsAnagramByDictionary(string s, string t)
        {
            // 1. If lengths are different → cannot be anagram
            if (s.Length != t.Length)
                return false;

            // 2. Dictionary to store character counts
            var map = new Dictionary<char, int>();

            // 3. Count characters from first string (s)
            foreach (char c in s)
            {
                // If character already exists → increase count
                // Else → add with count 1
                if (map.ContainsKey(c))
                    map[c]++;
                else
                    map[c] = 1;
            }

            // 4. Match characters from second string (t)
            foreach (char c in t)
            {
                // If character not found → extra character → not anagram
                if (!map.ContainsKey(c))
                    return false;

                // Decrease count (matching character from s)
                map[c]--;

                // If count becomes negative → more occurrences in t → not anagram
                if (map[c] < 0)
                    return false;
            }

            // 5. If we reached here → all matched
            return true;
        }
    }
}
