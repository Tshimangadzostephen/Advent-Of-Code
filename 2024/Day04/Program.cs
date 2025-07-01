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
    }

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
}
