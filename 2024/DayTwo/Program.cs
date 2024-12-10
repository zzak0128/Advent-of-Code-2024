internal class Program
{
    private static void Main(string[] args)
    {
        FileInfo inputFile = new(args[0]);
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
            if (IsLineSafe(lineItems))
            {
                safeCount++;
            }
        }

        System.Console.WriteLine($"the total number of safe lines = {safeCount}");

        safeCount = 0;
        foreach (var line in lines)
        {
            var lineItems = ParseLine(line);
            var isSafe = IsAnySafe(lineItems);

            if (isSafe)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("This line is deemed Safe");
                safeCount++;
                System.Console.WriteLine($"Current Safe Count {safeCount}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("This line is deemed NOT Safe");
            }

            Console.ForegroundColor = ConsoleColor.White;
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

    private static bool IsLineSafe(List<int> lineArray)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.Write("The Parsed Numbers Are:");
        foreach (var num in lineArray)
        {
            Console.Write(num);
            Console.Write(" ");
        }
        Console.WriteLine("");

        Console.ForegroundColor = ConsoleColor.White;

        bool lastWasIncrease = false;
        bool isIncrease = false;
        for (int i = 0; i < lineArray.Count(); i++)
        {
            if (i != lineArray.Count() - 1)
            {
                int num1 = lineArray[i];
                int num2 = lineArray[i + 1];
                int difference = 0;

                System.Console.WriteLine($"Checking numbers ({num1}, {num2})");

                // Check for current and next number difference
                if (num1 == num2)
                {
                    System.Console.WriteLine("numbers are equal. Not Safe.");

                    return false;
                }
                else if (num1 < num2)
                {
                    difference = num2 - num1;
                    isIncrease = true;
                    Console.WriteLine($"{num1} is less than {num2} and the difference is {difference}.");
                    if (i == 0)
                    {
                        lastWasIncrease = isIncrease;
                    }
                }
                else if (num1 > num2)
                {
                    difference = num1 - num2;
                    isIncrease = false;
                    Console.WriteLine($"{num1} is greater than {num2} and the difference is {difference}.");
                    if (i == 0)
                    {
                        lastWasIncrease = isIncrease;
                    }
                }

                if (difference > 3)
                {
                    Console.WriteLine($"The difference between numbers is {difference} and is greater than 3. Not Safe.");
                    return false;
                }

                if (lastWasIncrease != isIncrease)
                {
                    string last = lastWasIncrease ? "Ascending" : "Descending";
                    string current = isIncrease ? "Ascending" : "Descending";

                    Console.WriteLine($"The last two numbers were {last} the current two numbers are {current}. Not Safe.");
                    return false;
                }
            }
        }

        return true;
    }

    // this is for day two and is not working properly
    private static bool IsAnySafe(List<int> lineArray)
    {
        if (IsLineSafe(lineArray))
        {
            return true;
        }

        List<int> checkArray = [];
        foreach (var num in lineArray)
        {
            checkArray.Clear();
            checkArray.AddRange(lineArray);
            checkArray.Remove(num);

            if (IsLineSafe(checkArray))
            {
                return true;
            }
        }

        return false;
    }
}