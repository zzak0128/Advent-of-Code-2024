using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

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
        var currentTotal = 0;
            foreach (var line in lines)
            {
                currentTotal += ParseLine(line);
            }

            System.Console.WriteLine($"Current total = {currentTotal}");
    }

    private static int ParseLine(string line)
    {
        int lineTotal = 0;

        Console.WriteLine(line);
        Console.WriteLine("Matches:");
        var reg = new Regex(@"mul\(\d+\,\d+\)");
        var found = reg.Matches(line);
        foreach (var match in found)
        {
            Console.WriteLine(match);
            lineTotal += ParseAndCalculate(match);
        }

        return lineTotal;
    }

    private static int ParseAndCalculate(object mul)
    {
        var reg = new Regex(@"\d+");
        var matches = reg.Matches(mul.ToString());
        var number1 = matches.ElementAt(0).ToString();
        var number2 = matches.ElementAt(1).ToString();

        return Mul(int.Parse(number1), int.Parse(number2));
    }


    private static int Mul(int x, int y)
    {
        return x * y;
    }
}