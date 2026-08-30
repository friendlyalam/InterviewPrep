using InterviewPrep.DSA.DataStructures._01_Array.Problems._10_CountOccuranceOfGivenElement.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tests.Arrays
{
    public class CountOccuranceOfGivenElementTests
    {
        [Fact]
        public void Count_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;

            Assert.Throws<ArgumentNullException>(
                () => CountOccurrence.Count(input, 2));
        }

        [Fact]
        public void Count_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();

            Assert.Throws<ArgumentException>(
                () => CountOccurrence.Count(input, 2));
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 2, 2, 5 }, 2, 3)]
        [InlineData(new[] { 10, 20, 30, 40 }, 5, 0)]
        [InlineData(new[] { 5, 5, 5, 5 }, 5, 4)]
        [InlineData(new[] { -2, 3, -2, 0, -2 }, -2, 3)]
        [InlineData(new[] { 0, 0, 1, 2 }, 0, 2)]
        [InlineData(new[] { 7 }, 7, 1)]
        [InlineData(new[] { 7 }, 5, 0)]
        [InlineData(new[] { -1, -1, -1 }, -1, 3)]
        [InlineData(new[] { int.MinValue, 0, int.MinValue }, int.MinValue, 2)]
        [InlineData(new[] { int.MaxValue, 1, int.MaxValue }, int.MaxValue, 2)]
        public void Count_ShouldReturnCorrectOccurrenceCount(
            int[] input,
            int target,
            int expected)
        {
            int result = CountOccurrence.Count(input, target);

            Assert.Equal(expected, result);
        }
    }
}
