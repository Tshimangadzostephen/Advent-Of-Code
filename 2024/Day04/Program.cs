using System;
using System.IO;

class Program
{
    static readonly string Word = "XMAS";

    // Directions: right, left, down, up, down-right, down-left, up-right, up-left
    static readonly int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
    static readonly int[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };

    static void Main()
    {
        string inputFilePath = Path.Combine("..", "Data", "Day04", "Actual.txt");
        string[] lines = File.ReadAllLines(inputFilePath);
        char[][] grid = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            grid[i] = lines[i].ToCharArray();
        }

        int count = CountWordAllDirections(grid, Word);
        Console.WriteLine($"The word '{Word}' appears {count} times in all directions.");

        // --- Part 2 ---
        int part2Result = Count_XMas(grid);
        Console.WriteLine($"'X-MAS-es' found: {part2Result}");
    }

    // Part 1
    static int CountWordAllDirections(char[][] grid, string word)
    {
        int count = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                for (int dir = 0; dir < dx.Length; dir++)
                {
                    if (MatchesWord(grid, word, r, c, dx[dir], dy[dir]))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    static bool MatchesWord(char[][] grid, string word, int startX, int startY, int deltaX, int deltaY)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        for (int i = 0; i < word.Length; i++)
        {
            int x = startX + i * deltaX;
            int y = startY + i * deltaY;

            if (x < 0 || x >= rows || y < 0 || y >= cols)
                return false;

            if (grid[x][y] != word[i])
                return false;
        }

        return true;
    }

    // Part 2
    static int Count_XMas(char[][] grid)
    {
        int count = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        for (int x = 1; x < rows - 1; x++)
        {
            for (int y = 1; y < cols - 1; y++)
            {
                if (grid[x][y] != 'A')
                {
                    continue;
                }

                bool diag1 = IsMAS(grid[x - 1][y - 1], grid[x][y], grid[x + 1][y + 1]);
                bool diag2 = IsMAS(grid[x - 1][y + 1], grid[x][y], grid[x + 1][y - 1]);

                if (diag1 && diag2)
                {
                    count++;
                }
            }
        }
        return count;
    }

    // Checks if MAS in either forward or backward order
    static bool IsMAS(char c1, char c2, char c3)
    {
        string forward = $"{c1}{c2}{c3}";
        string backward = $"{c3}{c2}{c1}";

        return forward == "MAS" || backward == "MAS";
    }
}