using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        FileInfo inputFile = new(args[0]);
        string input = "";
        using (var reader = new StreamReader(inputFile.FullName))
        {
            input = reader.ReadToEnd();
        }

        //var currentTotal = ParseInput(input);
        var currentTotal = ParseInput_PartTwo(input);

        Console.WriteLine($"Current total = {currentTotal}");
    }

    private static int ParseInput(string input)
    {
        int inputTotal = 0;

        Console.WriteLine(input);
        Console.WriteLine("Matches:");

        var reg = new Regex(@"mul\(\d+\,\d+\)");
        var foundMul = reg.Matches(input);

        foreach (Match match in foundMul)
        {
            var matchIndex = match.Index;

            Console.WriteLine(match);
            inputTotal += ParseAndCalculate(match);
        }

        return inputTotal;
    }

    private static int ParseInput_PartTwo(string input)
    {
        int inputTotal = 0;

        Console.WriteLine(input);
        Console.WriteLine("Matches:");

        var reg = new Regex(@"mul\(\d+\,\d+\)");
        var foundMul = reg.Matches(input);

        var regDo = new Regex(@"do\(\)");
        var regDont = new Regex(@"don't\(\)");

        var doMatches = regDo.Matches(input);
        var dontMatches = regDont.Matches(input);

        var doIndexes = doMatches.Select(x => x.Index).ToList();
        var dontIndexes = dontMatches.Select(x => x.Index).ToList();
        var mergedIndices = doIndexes.Concat(dontIndexes).OrderBy(x => x).ToArray();

        var isEnabled = true;
        var nextIndex = 0;
        foreach (Match match in foundMul)
        {
            var matchIndex = match.Index;
            if (nextIndex < mergedIndices.Length && matchIndex > mergedIndices[nextIndex])
            {
                var isDo = doIndexes.Contains(mergedIndices[nextIndex]);
                isEnabled = isDo ? true : false;
                nextIndex++;
            }

            Console.WriteLine(match);

            if (isEnabled)
            {
                inputTotal += ParseAndCalculate(match);
            }
        }

        return inputTotal;
    }

    private static int ParseAndCalculate(object mul)
    {
        var reg = new Regex(@"\d+");
        var matches = reg.Matches(mul.ToString());
        var number1 = matches.ElementAt(0).Value;
        var number2 = matches.ElementAt(1).Value;

        return Mul(int.Parse(number1), int.Parse(number2));
    }

    private static int Mul(int x, int y)
    {
        return x * y;
    }
}