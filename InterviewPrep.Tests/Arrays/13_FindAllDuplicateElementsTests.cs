using InterviewPrep.DSA.DataStructures._01_Array.Problems._13_FindAllDuplicateElements.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class FindAllDuplicateElementsTests
    {
        [Fact]
        public void Find_NullInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FindAllDuplicateElements.Find(null!));
        }

        [Fact]
        public void Find_EmptyInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => FindAllDuplicateElements.Find(Array.Empty<int>()));
        }

        [Theory]
        [InlineData(new[] { 4, 3, 2, 7, 8, 2, 3, 1, 3 }, new[] { 2, 3 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new int[0])]
        [InlineData(new[] { 9, 9, 9, 9 }, new[] { 9 })]
        [InlineData(new[] { -5, 0, -5, 10, 0 }, new[] { -5, 0 })]
        [InlineData(new[] { 42 }, new int[0])]
        public void Find_ValidInputs_ReturnsExpectedDuplicates(int[] input, int[] expected)
        {
            int[] result = FindAllDuplicateElements.Find(input);
            Assert.Equal(expected.Length, result.Length);
            Assert.All(expected, item => Assert.Contains(item, result));
        }
    }
}
