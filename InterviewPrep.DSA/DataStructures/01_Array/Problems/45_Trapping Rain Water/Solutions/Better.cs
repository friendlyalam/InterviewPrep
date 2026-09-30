namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._45_TrappingRainWater.Solutions
{

    // Better: Prefix/Suffix Maximum
    public static class TrappingRainWaterBetter
    {
        public static int Calculate(int[] height)
        {
            ValidateInput(height);

            int n = height.Length;

            if (n < 3)
                return 0;

            int[] leftMax = new int[n];
            int[] rightMax = new int[n];

            // Build maximum height seen from the left.
            leftMax[0] = height[0];

            for (int i = 1; i < n; i++)
            {
                leftMax[i] = Math.Max(
                    leftMax[i - 1],
                    height[i]);
            }

            // Build maximum height seen from the right.
            rightMax[n - 1] = height[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                rightMax[i] = Math.Max(
                    rightMax[i + 1],
                    height[i]);
            }

            int totalWater = 0;

            for (int i = 0; i < n; i++)
            {
                // Water level is limited by the shorter side.
                int waterLevel = Math.Min(
                    leftMax[i],
                    rightMax[i]);

                totalWater += waterLevel - height[i];
            }

            return totalWater;
        }

        private static void ValidateInput(int[] height)
        {
            if (height is null)
                throw new ArgumentNullException(nameof(height));

            if (height.Length == 0)
                throw new ArgumentException(
                    "height cannot be empty.",
                    nameof(height));

            for (int i = 0; i < height.Length; i++)
            {
                if (height[i] < 0)
                    throw new ArgumentException(
                        "Height values cannot be negative.",
                        nameof(height));
            }
        }
    }
}
