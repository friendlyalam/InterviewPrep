namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._42_SpiralMatrix.Solutions
{
    //Optimal Solution — Boundary Traversal
    public static class SpiralMatrixOptimal
    {
        public static IList<int> Traverse(int[][] matrix)
        {
            ValidateMatrix(matrix);

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            List<int> result = new(rows * columns);

            // These four variables represent the current
            // unvisited rectangle.
            int top = 0;
            int bottom = rows - 1;
            int left = 0;
            int right = columns - 1;

            while (top <= bottom && left <= right)
            {
                // -------------------------------------------------
                // STEP 1: Traverse from LEFT -> RIGHT
                // -------------------------------------------------

                for (int column = left; column <= right; column++)
                {
                    result.Add(matrix[top][column]);
                }

                // The top row has now been completely processed.
                top++;

                // -------------------------------------------------
                // STEP 2: Traverse from TOP -> BOTTOM
                // -------------------------------------------------

                for (int row = top; row <= bottom; row++)
                {
                    result.Add(matrix[row][right]);
                }

                // The right column has now been processed.
                right--;

                // -------------------------------------------------
                // STEP 3: Traverse from RIGHT -> LEFT
                //
                // We must first check that a row still exists.
                // This is important for single-row matrices.
                // -------------------------------------------------

                if (top <= bottom)
                {
                    for (int column = right;
                         column >= left;
                         column--)
                    {
                        result.Add(matrix[bottom][column]);
                    }

                    // The bottom row has now been processed.
                    bottom--;
                }

                // -------------------------------------------------
                // STEP 4: Traverse from BOTTOM -> TOP
                //
                // We must check that a column still exists.
                // This is important for single-column matrices.
                // -------------------------------------------------

                if (left <= right)
                {
                    for (int row = bottom;
                         row >= top;
                         row--)
                    {
                        result.Add(matrix[row][left]);
                    }

                    // The left column has now been processed.
                    left++;
                }
            }

            return result;
        }

        private static void ValidateMatrix(int[][] matrix)
        {
            if (matrix is null)
                throw new ArgumentNullException(nameof(matrix));

            if (matrix.Length == 0)
                throw new ArgumentException(
                    "matrix cannot be empty.",
                    nameof(matrix));

            if (matrix[0] is null)
                throw new ArgumentException(
                    "Matrix rows cannot be null.",
                    nameof(matrix));

            if (matrix[0].Length == 0)
                throw new ArgumentException(
                    "Matrix must contain at least one column.",
                    nameof(matrix));

            int columns = matrix[0].Length;

            for (int row = 0; row < matrix.Length; row++)
            {
                if (matrix[row] is null)
                {
                    throw new ArgumentException(
                        $"Row {row} cannot be null.",
                        nameof(matrix));
                }

                if (matrix[row].Length != columns)
                {
                    throw new ArgumentException(
                        "Matrix must be rectangular.",
                        nameof(matrix));
                }
            }
        }
    }
}
