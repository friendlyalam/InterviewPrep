namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._45_TrappingRainWater.Solutions
{
    //Optimal: Two Pointers
    public static class TrappingRainWaterOptimal
    {
        public static int Calculate(int[] height)
        {
            ValidateInput(height);

            if (height.Length < 3)
                return 0;

            int left = 0;
            int right = height.Length - 1;

            int leftMax = 0;
            int rightMax = 0;

            long totalWater = 0;

            while (left < right)
            {
                // The smaller boundary determines the water
                // that can currently be calculated.
                if (height[left] <= height[right])
                {
                    if (height[left] >= leftMax)
                    {
                        leftMax = height[left];
                    }
                    else
                    {
                        totalWater += leftMax - height[left];
                    }

                    left++;
                }
                else
                {
                    if (height[right] >= rightMax)
                    {
                        rightMax = height[right];
                    }
                    else
                    {
                        totalWater += rightMax - height[right];
                    }

                    right--;
                }
            }

            return checked((int)totalWater);
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
