
namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._18_BestTimeToBuyAndSellStock.Solutions
{
    public class BestTimeToBuyAndSellStockOptimal
    {
        public static int MaxProfit(int[] prices)
        {
            if (prices is null)
                throw new ArgumentNullException(nameof(prices));

            if (prices.Length == 0)
                throw new ArgumentException("prices cannot be empty.", nameof(prices));

            // Lowest price encountered so far.
            int minBuyPrice = prices[0];

            // Maximum profit found so far.
            int maxProfit = 0;

            // Start from the second day because prices[0] is our initial buy price.
            for (int i = 1; i < prices.Length; i++)
            {
                // If today's price is lower, it becomes the better buying opportunity.
                if (prices[i] < minBuyPrice)
                {
                    minBuyPrice = prices[i];
                }
                else
                {
                    // Calculate profit by selling today.
                    int profit = prices[i] - minBuyPrice;

                    // Keep the highest profit found so far.
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                    }
                }
            }

            // Returns 0 when no profitable transaction is possible.
            return maxProfit;
        }
    }
}
