namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._41_SetMatrixZeroes.Solutions
{
    // use separate row/column markers → O(m × n) time, O(m + n) space.
    //The two marker arrays store which rows and columns need to become zero.
    public class SetMatrixZeroesBetter
    {
        public static void SetZeroes(int[][] matrix)
        {
            ValidateMatrix(matrix);

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            // These arrays remember which rows and columns
            // originally contained zero.
            bool[] zeroRows = new bool[rows];
            bool[] zeroColumns = new bool[columns];

            // Step 1:
            // Find all zeroes in the ORIGINAL matrix.
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (matrix[row][column] == 0)
                    {
                        zeroRows[row] = true;
                        zeroColumns[column] = true;
                    }
                }
            }

            // Step 2:
            // Set entire rows to zero.
            for (int row = 0; row < rows; row++)
            {
                if (!zeroRows[row])
                    continue;

                for (int column = 0; column < columns; column++)
                {
                    matrix[row][column] = 0;
                }
            }

            // Step 3:
            // Set entire columns to zero.
            for (int column = 0; column < columns; column++)
            {
                if (!zeroColumns[column])
                    continue;

                for (int row = 0; row < rows; row++)
                {
                    matrix[row][column] = 0;
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
