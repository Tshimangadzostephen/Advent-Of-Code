using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("..", "Data", "Day03", "Actual.txt");
        string fileContents = File.ReadAllText(inputFilePath);

        int part1Total = RunPart1(fileContents);
        int part2Total = RunPart2(fileContents);

        Console.WriteLine($"[Part 1] Total sum of all valid multiplications: {part1Total}");
        Console.WriteLine($"[Part 2] Total sum of ENABLED multiplications: {part2Total}");
    }

    static int RunPart1(string fileContents)
    {
        // Match only valid mul(X,Y)
        var mulPattern = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        int total = 0;

        foreach (Match match in mulPattern.Matches(fileContents))
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            total += x * y;
        }

        return total;
    }

    static int RunPart2(string fileContents)
    {
        // Match do(), don't(), and valid mul(X,Y)
        var pattern = new Regex(@"do\(\)|don't\(\)|mul\((\d{1,3}),(\d{1,3})\)");
        bool mulEnabled = true;
        int total = 0;

        foreach (Match match in pattern.Matches(fileContents))
        {
            string value = match.Value;

            if (value == "do()")
            {
                mulEnabled = true;
            }
            else if (value == "don't()")
            {
                mulEnabled = false;
            }
            else if (match.Groups[1].Success && match.Groups[2].Success)
            {
                if (mulEnabled)
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    total += x * y;
                }
            }
        }

        return total;
    }
}
