class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("..", "Data", "Day06", "Actual.txt");
        string[] lines = File.ReadAllLines(inputFilePath);

        int rows = lines.Length;
        int cols = lines[0].Length;
        char[,] map = new char[rows, cols];

        // 0 = up, 1 = right, 2 = down, 3 = left
        int direction = 0;
        int row = 0;
        int col = 0;


        // Fill map and find guard
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                char ch = lines[r][c];
                map[r, c] = ch;

                if (ch == '^')
                {
                    row = r;
                    col = c;
                    direction = 0;
                }
                else if (ch == '>')
                {
                    row = r;
                    col = c;
                    direction = 1;
                }
                else if (ch == 'v')
                {
                    row = r;
                    col = c;
                    direction = 2;
                }
                else if (ch == '<')
                {
                    row = r;
                    col = c;
                    direction = 3;
                }

            }
        }

        bool[,] visited = new bool[rows, cols];
        visited[row, col] = true;

        while (true)
        {
            int newRow = row, newCol = col;

            if (direction == 0)
                newRow--; // up
            else if (direction == 1)
                newCol++; // right
            else if (direction == 2)
                newRow++; // down
            else if (direction == 3)
                newCol--; // left

            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
            {
                break;
            }

            if (map[newRow, newCol] == '#')
            {
                direction = (direction + 1) % 4; // turn right
            }
            else
            {
                row = newRow;
                col = newCol;
                visited[row, col] = true;
            }
        }

        int count = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (visited[r, c]) count++;
            }
        }

        Console.WriteLine("Total positions visited: " + count);
    }
}
