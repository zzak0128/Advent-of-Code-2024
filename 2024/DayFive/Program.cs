using DayFive;

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

        ParsedUpdates parsedUpdates = ParseLines(lines);
        var checkedLists = CheckLists(parsedUpdates, ReturnType.Valid);

        int midTotal = 0;
        foreach (var list in checkedLists)
        {
            midTotal += FindCenterNumber(list);
        }

        Console.WriteLine($"The total of all valid center numbers = {midTotal}");

        midTotal = 0;
        List<List<int>> sortedLists = [];
        foreach (var list in CheckLists(parsedUpdates, ReturnType.Invalid))
        {
            bool isListValid = false;
            List<int> sortedList = [];
            while (!isListValid)
            {
                sortedList = SortList(list, parsedUpdates.SortOrders);
                isListValid = IsListOrdered(sortedList, parsedUpdates.SortOrders);
            }

            sortedLists.Add(sortedList);
        }

        foreach (var sortedList in sortedLists)
        {
            midTotal += FindCenterNumber(sortedList);
        }

        Console.WriteLine($"The added total of the invalid lists = {midTotal}");
    }

    private static ParsedUpdates ParseLines(List<string> lines)
    {
        ParsedUpdates updates = new();

        foreach (var line in lines)
        {
            if (line.Contains('|'))
            {
                var split = line.Split('|');
                if (updates.SortOrders.ContainsKey(int.Parse(split[0])))
                {
                    updates.SortOrders[int.Parse(split[0])].Add(int.Parse(split[1]));
                }
                else
                {
                    List<int> newList = [];
                    newList.Add(int.Parse(split[1]));
                    updates.SortOrders.Add(int.Parse(split[0]), newList);
                }
            }
            else if (line.Contains(','))
            {
                var numbers = line.Split(',');
                List<int> newUpdate = [];
                foreach (var number in numbers)
                {
                    newUpdate.Add(int.Parse(number));
                }

                updates.UpdateLines.Add(newUpdate);
            }
        }

        return updates;
    }

    private static bool IsListOrdered(List<int> nums, Dictionary<int, List<int>> sortRules)
    {
        bool output = true;

        foreach (var num in nums)
        {
            if (sortRules.TryGetValue(num, out List<int>? sortNums))
            {
                int numIndex = nums.IndexOf(num);
                foreach (var sortNum in sortNums)
                {
                    if (nums.IndexOf(sortNum) < numIndex && nums.IndexOf(sortNum) > -1)
                    {
                        return false;
                    }
                }
            }
        }

        return output;
    }

    private static List<List<int>> CheckLists(ParsedUpdates updates, ReturnType returnType)
    {
        List<List<int>> sortedLists = [];
        foreach (var line in updates.UpdateLines)
        {
            bool isLineInOrder = IsListOrdered(line, updates.SortOrders);

            switch (returnType)
            {
                case ReturnType.Valid:
                    if (isLineInOrder)
                    {
                        sortedLists.Add(line);
                    }
                    break;
                case ReturnType.Invalid:
                    if (!isLineInOrder)
                    {
                        sortedLists.Add(line);
                    }
                    break;
                default:
                    break;
            }
        }

        return sortedLists;
    }

    private static List<int> SortList(List<int> nums, Dictionary<int, List<int>> sortRules)
    {
        Random rand = new Random();

        // I am aware that this is about the worst possible solution
        // I spent enough time on this and would like to move on :)
        List<int> output = nums;
        for (int i = output.Count - 1; i > 0; i--)
        {
            var k = rand.Next(i + 1);
            var value = output[k];
            output[k] = output[i];
            output[i] = value;
        }

        return output;
    }

    private static int FindCenterNumber(List<int> nums)
    {
        int length = nums.Count - 1;
        int midIndex = length / 2;

        return nums[midIndex];
    }
}