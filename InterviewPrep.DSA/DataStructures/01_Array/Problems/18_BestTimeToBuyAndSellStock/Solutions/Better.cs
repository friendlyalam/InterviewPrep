
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._18_BestTimeToBuyAndSellStock.Solutions
{
    public class BestTimeToBuyAndSellStockBetter
    {
        public static int MaxProfit(int[] prices)
        {
            if (prices is null)
                throw new ArgumentNullException(nameof(prices));

            if (prices.Length == 0)
                throw new ArgumentException("prices cannot be empty.", nameof(prices));

            int maxProfit = 0;

            // Try every possible buying day.
            for (int buyDay = 0; buyDay < prices.Length - 1; buyDay++)
            {
                // Selling must happen after buying.
                for (int sellDay = buyDay + 1; sellDay < prices.Length; sellDay++)
                {
                    // Calculate profit for this buy/sell combination.
                    int profit = prices[sellDay] - prices[buyDay];

                    // Keep the maximum profitable transaction.
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                    }
                }
            }

            // If no profitable transaction exists, maxProfit remains 0.
            return maxProfit;
        }
    }
}
