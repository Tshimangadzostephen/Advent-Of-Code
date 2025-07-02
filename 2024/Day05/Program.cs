class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("..", "Data", "Day05", "Actual.txt");
        var lines = File.ReadAllLines(inputFilePath);

        var rules = new List<(int X, int Y)>();
        var updates = new List<List<int>>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.Contains("|"))
            {
                var parts = line.Split('|');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                rules.Add((x, y));
            }
            else if (line.Contains(","))
            {
                var update = line.Split(',').Select(int.Parse).ToList();
                updates.Add(update);
            }
        }

        int median_Sums = 0;

        foreach (var update in updates)
        {
            if (IsValidUpdate(update, rules))
            {
                int middleIndex = update.Count / 2;
                median_Sums += update[middleIndex];
            }
        }

        Console.WriteLine($"Total of middle pages in valid updates: {median_Sums}");

        FixAndSumInvalidUpdates(updates, rules);
    }

    static bool IsValidUpdate(List<int> update, List<(int X, int Y)> rules)
    {
        var positions = new Dictionary<int, int>();

        for (int i = 0; i < update.Count; i++)
        {
            positions[update[i]] = i;
        }

        foreach (var (x, y) in rules)
        {
            if (positions.ContainsKey(x) && positions.ContainsKey(y))
            {
                if (positions[x] >= positions[y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    //Part 2
    static void FixAndSumInvalidUpdates(List<List<int>> updates, List<(int X, int Y)> rules)
    {
        int sumOfFixedMiddlePages = 0;

        foreach (var update in updates)
        {
            if (!IsValidUpdate(update, rules))
            {
                var sorted = SortUpdateByRules(update, rules);
                int middleIndex = sorted.Count / 2;
                sumOfFixedMiddlePages += sorted[middleIndex];
            }
        }

        Console.WriteLine($"Total of middle pages in fixed invalid updates: {sumOfFixedMiddlePages}");
    }

    static List<int> SortUpdateByRules(List<int> update, List<(int X, int Y)> rules)
    {
        // Make a copy to sort
        var sorted = new List<int>(update);

        // using a do while loop for this bubble sort because I don't know the value of n(passes)
        //!look into topological sort as well
        bool changed;
        do
        {
            changed = false;

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                int a = sorted[i];
                int b = sorted[i + 1];

                // Check if there is a rule that says b must come before a (b | a)
                if (rules.Any(rule => rule.X == b && rule.Y == a))
                {
                    sorted[i] = b;
                    sorted[i + 1] = a;
                    changed = true;
                }
            }

        } while (changed); // Keep looping until no more swaps are needed

        return sorted;
    }

}
