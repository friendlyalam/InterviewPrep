using InterviewPrep.DSA.DataStructures._01_Array.Problems._18_BestTimeToBuyAndSellStock.Solutions;

namespace InterviewPrep.Tests.Arrays
{
    public class BestTimeToBuyAndSellStockTests
    {
        [Theory]
        [InlineData(new int[] { 7, 1, 5, 3, 6, 4 }, 5)]
        [InlineData(new int[] { 7, 6, 4, 3, 1 }, 0)]
        [InlineData(new int[] { 2, 4, 1 }, 2)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new int[] { 5 }, 0)]
        [InlineData(new int[] { 7, 6, 4, 1, 5 }, 4)]
        [InlineData(new int[] { 3, 3, 3, 3 }, 0)]
        [InlineData(new int[] { 2, 1, 2, 1, 5 }, 4)]
        public void MaxProfit_ShouldReturnExpectedProfit(
            int[] prices,
            int expected)
        {
            // Act
            int result = BestTimeToBuyAndSellStockOptimal.MaxProfit(prices);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MaxProfit_ShouldThrowArgumentNullException_WhenPricesIsNull()
        {
            // Arrange
            int[] prices = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => BestTimeToBuyAndSellStockOptimal.MaxProfit(prices));
        }

        [Fact]
        public void MaxProfit_ShouldThrowArgumentException_WhenPricesIsEmpty()
        {
            // Arrange
            int[] prices = Array.Empty<int>();

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => BestTimeToBuyAndSellStockOptimal.MaxProfit(prices));
        }
    }
}
