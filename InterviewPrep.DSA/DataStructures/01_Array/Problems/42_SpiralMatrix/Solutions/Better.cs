namespace InterviewPrep.DSA.DataStructures._01_Array.Problems._42_SpiralMatrix.Solutions
{

    //Better Solution — Visited Matrix
    public static class SpiralMatrixBetter
    {
        public static IList<int> Traverse(int[][] matrix)
        {
            ValidateMatrix(matrix);

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            // Keeps track of whether each cell has already been visited.
            bool[,] visited = new bool[rows, columns];

            List<int> result = new(rows * columns);

            // Directions:
            // 0 = right
            // 1 = down
            // 2 = left
            // 3 = up
            int[] rowDirections = { 0, 1, 0, -1 };
            int[] columnDirections = { 1, 0, -1, 0 };

            int currentRow = 0;
            int currentColumn = 0;
            int direction = 0;

            for (int count = 0; count < rows * columns; count++)
            {
                // Visit the current cell.
                result.Add(matrix[currentRow][currentColumn]);
                visited[currentRow, currentColumn] = true;

                int nextRow =
                    currentRow + rowDirections[direction];

                int nextColumn =
                    currentColumn + columnDirections[direction];

                // Change direction when:
                // 1. We reach a boundary.
                // 2. The next cell was already visited.
                if (nextRow < 0 ||
                    nextRow >= rows ||
                    nextColumn < 0 ||
                    nextColumn >= columns ||
                    visited[nextRow, nextColumn])
                {
                    direction = (direction + 1) % 4;

                    nextRow =
                        currentRow + rowDirections[direction];

                    nextColumn =
                        currentColumn + columnDirections[direction];
                }

                currentRow = nextRow;
                currentColumn = nextColumn;
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
