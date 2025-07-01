class Program
{
    static void Main()
    {
        //string filePath = Path.Combine("..", "Data", "Day01", "Demo.txt");
        string filePath = Path.Combine("..", "Data", "Day01", "Actual.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Input file not found.");
            return;
        }

        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();

        foreach (var line in File.ReadLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into two parts
            var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 && int.TryParse(parts[0], out int leftVal) && int.TryParse(parts[1], out int rightVal))
            {
                leftList.Add(leftVal);
                rightList.Add(rightVal);
            }
        }

        if (leftList.Count != rightList.Count)
        {
            Console.WriteLine("The two lists are not the same length.");
            return;
        }

        // PART 1: Calculate the total distance between the two lists
        leftList.Sort();
        rightList.Sort();

        int totalDistance = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            totalDistance += Math.Abs(leftList[i] - rightList[i]);
        }

        Console.WriteLine($"Total distance: {totalDistance}");

        //PART 2: Calculate similarity score
        var rightFrequencies = new Dictionary<int, int>();
        foreach (int num in rightList)
        {
            if (!rightFrequencies.ContainsKey(num))
            {
                rightFrequencies[num] = 0;
            }
            rightFrequencies[num]++;
        }

        int similarityScore = 0;
        foreach (int num in leftList)
        {
            if (rightFrequencies.TryGetValue(num, out int count))
            {
                similarityScore += num * count;
            }
        }

        Console.WriteLine($"Similarity score: {similarityScore}");

    }
}
