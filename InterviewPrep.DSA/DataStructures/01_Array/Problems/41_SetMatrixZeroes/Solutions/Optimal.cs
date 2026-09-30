namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._41_SetMatrixZeroes.Solutions
{
    //use the first row and first column as markers → O(m × n) time, O(1) extra space.
    public static class SetMatrixZeroesOptimal
    {
        public static void SetZeroes(int[][] matrix)
        {
            ValidateMatrix(matrix);

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            // These flags are necessary because the first row
            // and first column themselves are being used as markers.
            bool firstRowHasZero = false;
            bool firstColumnHasZero = false;

            // ---------------------------------------------------------
            // Step 1:
            // Check whether the FIRST ROW originally contained zero.
            // ---------------------------------------------------------

            for (int column = 0; column < columns; column++)
            {
                if (matrix[0][column] == 0)
                {
                    firstRowHasZero = true;
                    break;
                }
            }

            // ---------------------------------------------------------
            // Step 2:
            // Check whether the FIRST COLUMN originally contained zero.
            // ---------------------------------------------------------

            for (int row = 0; row < rows; row++)
            {
                if (matrix[row][0] == 0)
                {
                    firstColumnHasZero = true;
                    break;
                }
            }

            // ---------------------------------------------------------
            // Step 3:
            // Use the first row and first column as markers.
            //
            // Start from row 1 and column 1 because row 0 and
            // column 0 are being used to store marker information.
            // ---------------------------------------------------------

            for (int row = 1; row < rows; row++)
            {
                for (int column = 1; column < columns; column++)
                {
                    if (matrix[row][column] == 0)
                    {
                        // Mark this row.
                        matrix[row][0] = 0;

                        // Mark this column.
                        matrix[0][column] = 0;
                    }
                }
            }

            // ---------------------------------------------------------
            // Step 4:
            // Use the markers to zero the internal matrix.
            //
            // We start from row 1 and column 1 again because
            // first row/column are still being used as markers.
            // ---------------------------------------------------------

            for (int row = 1; row < rows; row++)
            {
                for (int column = 1; column < columns; column++)
                {
                    if (matrix[row][0] == 0 ||
                        matrix[0][column] == 0)
                    {
                        matrix[row][column] = 0;
                    }
                }
            }

            // ---------------------------------------------------------
            // Step 5:
            // Finally process the FIRST ROW if it originally
            // contained zero.
            // ---------------------------------------------------------

            if (firstRowHasZero)
            {
                for (int column = 0; column < columns; column++)
                {
                    matrix[0][column] = 0;
                }
            }

            // ---------------------------------------------------------
            // Step 6:
            // Finally process the FIRST COLUMN if it originally
            // contained zero.
            // ---------------------------------------------------------

            if (firstColumnHasZero)
            {
                for (int row = 0; row < rows; row++)
                {
                    matrix[row][0] = 0;
                }
            }
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
