using InterviewPrep.DSA.DataStructures._01_Array.Problems._03_SumOfArrays.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Tests.Arrays
{
    public class _03_SumOfArraysTests
    {
        [Fact]
        public void Sum_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            int[] input = null!;
            Assert.Throws<ArgumentNullException>(() => SumOfArrays.Sum(input));
        }

        [Fact]
        public void Sum_ShouldThrowArgumentException_WhenInputIsEmpty()
        {
            int[] input = Array.Empty<int>();
            Assert.Throws<ArgumentException>(() => SumOfArrays.Sum(input));
        }

        [Theory]
        [InlineData(new[] { 50 }, 50)]
        [InlineData(new[] { 10, 20, 30, 40 }, 100)]
        [InlineData(new[] { -5, 10, -3 }, 2)]
        [InlineData(new[] { 0, 0, 0 }, 0)]
        [InlineData(new[] { -10, -20, -30 }, -60)]
        [InlineData(new[] { 10, -5, 20, -10 }, 15)]
        public void ShouldReturnExpectedOutput(int[] input, int expected)
        {
            int result = SumOfArrays.Sum(input);
            Assert.Equal(expected, result);
        }

    }
}
