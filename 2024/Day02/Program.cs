class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("..", "Data", "Day02", "Actual.txt");

        // Read all reports (lines) from the file
        string[] reports = File.ReadAllLines(inputFilePath);

        int safeCount = 0;
        int safeCountPart2 = 0;


        foreach (string report in reports)
        {
            // Parse levels as integers
            int[] levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(int.Parse)
                                 .ToArray();

            if (IsSafeReport(levels))
            {
                safeCount++;
                safeCountPart2++;

            }
            else if (IsSafeWithOneRemoved(levels))
            {
                safeCountPart2++;
            }
        }

        Console.WriteLine($"Number of safe reports: {safeCount}");
        Console.WriteLine($"Number of safe reports (Part 2): {safeCountPart2}");
    }

    static bool IsSafeReport(int[] levels)
    {
        if (levels.Length < 2)
            return true;

        // Determine if increasing or decreasing by comparing first pair
        bool? increasing = null;

        for (int i = 1; i < levels.Length; i++)
        {
            int diff = levels[i] - levels[i - 1];

            // Check difference (between 1 and 3)
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                return false;

            // Check for diff == 0
            if (diff == 0)
                return false;

            // Determine increasing or decreasing pattern
            if (increasing == null)
            {
                increasing = diff > 0;
            }
            else
            {
                // If pattern breaks, return false
                if ((diff > 0) != increasing)
                    return false;
            }
        }

        return true;
    }

    static bool IsSafeWithOneRemoved(int[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int[] reduced = levels.Where((val, idx) => idx != i).ToArray();

            if (IsSafeReport(reduced))
                return true;
        }

        return false;
    }
}
