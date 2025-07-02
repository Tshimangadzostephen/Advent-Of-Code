class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("..", "Data", "Day07", "Actual.txt");
        var lines = File.ReadAllLines(inputFilePath);

        long totalCalibrationSumPart1 = 0;
        long totalCalibrationSumPart2 = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(':');
            long targetValue = long.Parse(parts[0].Trim());

            var numberStrings = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long[] numbers = Array.ConvertAll(numberStrings, long.Parse);

            // Part 1: + and *
            if (CanProduceTargetValue(numbers, targetValue))
            {
                totalCalibrationSumPart1 += targetValue;
            }

            // Part 2: +, *, and ||
            if (CanProduceTargetValuePart2(numbers, targetValue))
            {
                totalCalibrationSumPart2 += targetValue;
            }
        }

        Console.WriteLine($"Part 1 - Total calibration result: {totalCalibrationSumPart1}");
        Console.WriteLine($"Part 2 - Total calibration result: {totalCalibrationSumPart2}");
    }

    // Part 1
    static bool CanProduceTargetValue(long[] numbers, long targetValue)
    {
        int operatorSlotCount = numbers.Length - 1;
        int totalOperatorCombinations = 1 << operatorSlotCount; // 2 slots for + or *

        for (int combination = 0; combination < totalOperatorCombinations; combination++)
        {
            long result = numbers[0];

            for (int position = 0; position < operatorSlotCount; position++)
            {
                bool useMultiplication = ((combination >> position) & 1) == 1;
                long nextNumber = numbers[position + 1];

                if (useMultiplication)
                {
                    result *= nextNumber;
                }
                else
                {
                    result += nextNumber;
                }
            }

            if (result == targetValue)
                return true;
        }

        return false;
    }

    // Part 2:
    static bool CanProduceTargetValuePart2(long[] numbers, long targetValue)
    {
        int operatorSlots = numbers.Length - 1;
        long totalCombinations = (long)Math.Pow(3, operatorSlots); // 3 slots for +, *, ||

        for (long combination = 0; combination < totalCombinations; combination++)
        {
            long result = numbers[0];
            long tempCombination = combination;

            for (int position = 0; position < operatorSlots; position++)
            {
                int operatorChoice = (int)(tempCombination % 3);
                tempCombination /= 3;

                long nextNumber = numbers[position + 1];

                if (operatorChoice == 0)
                {
                    // Addition
                    result += nextNumber;
                }
                else if (operatorChoice == 1)
                {
                    // Multiplication
                    result *= nextNumber;
                }
                else if (operatorChoice == 2)
                {
                    // Concatenation
                    string concatStr = result.ToString() + nextNumber.ToString();
                    if (!long.TryParse(concatStr, out result))
                    {
                        // If concatenation overflows long, skip this combination early
                        result = long.MinValue;
                        break;
                    }
                }
            }

            if (result == targetValue)
                return true;
        }

        return false;
    }
}
