internal class Program
{
    private static void Main(string[] args)
    {
        FileInfo inputFile = new(args[0]);
        //FileInfo inputFile = new(@"/home/zzak/Downloads/DayTwoPracticeInput.txt");
        List<string> lines = [];
        using (var reader = new StreamReader(inputFile.FullName))
        {
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }
        }

        int safeCount = 0;
        foreach (var line in lines)
        {
            var lineItems = ParseLine(line);
            if (IsSafe(lineItems))
            {
                safeCount++;
            }
        }

        System.Console.WriteLine($"the total number of safe lines = {safeCount}");

        foreach (var line in lines)
        {
            var lineItems = ParseLine(line);
            if (IsAnySafe(lineItems))
            {
                safeCount++;
            }
        }

        System.Console.WriteLine($"the total number of safe lines after removing = {safeCount}");

    }

    private static List<int> ParseLine(string line)
    {
        List<int> numbers = [];

        var lineItems = line.Split(' ');
        foreach (var item in lineItems)
        {
            numbers.Add(int.Parse(item));
        }

        return numbers;
    }

    private static bool IsSafe(List<int> lineArray)
    {
        bool lastIncrease = false;
        bool currentIncrease = false;
        for (int i = 0; i < lineArray.Count(); i++)
        {
            if (i != lineArray.Count() - 1)
            {
                int num1 = lineArray[i];
                int num2 = lineArray[i + 1];
                int difference = num1 - num2;

                if (num1 == num2)
                {
                    return false;
                }

                if (num1 < num2)
                {
                    currentIncrease = true;
                }

                if (num1 > num2)
                {
                    currentIncrease = false;
                }

                if(currentIncrease)
                {
                    difference = difference * -1;
                }

                if (difference > 3)
                {
                    return false;
                }

                if (i == 0)
                {
                    lastIncrease = currentIncrease;
                }
                else if (lastIncrease != currentIncrease)
                {
                    return false;
                }
            }
        }

        return true;
    }

// this is for day two and is not working properly
    private static bool IsAnySafe(List<int> lineArray)
    {
        var isSafe = IsSafe(lineArray);
        if (!isSafe)
        {
            List<int> checkArray = [];
            foreach (var num in lineArray)
            {
                checkArray.Clear();
                checkArray.AddRange(lineArray);
                checkArray.Remove(num);

                isSafe = IsSafe(checkArray);

            }
        }

        return isSafe;
    }
}