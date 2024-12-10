internal class Program
{
    private static void Main(string[] args)
    {
        FileInfo inputFile = new(args[0]);
        IEnumerable<string> lines = [];
        using (var reader = new StreamReader(inputFile.FullName))
        {
            while (!reader.EndOfStream)
            {
                lines = lines.Append(reader.ReadLine());
            }
        }
        
        int finalTotal = 0;

        IEnumerable<int> listOne = [];
        IEnumerable<int> listTwo = [];   

        foreach(var line in lines)
        {
            string[] lineSplit = line.Split(' ');
            listOne = listOne.Append(int.Parse(lineSplit[0]));
            listTwo = listTwo.Append(int.Parse(lineSplit[3]));
        }

        var arrayOne = listOne.Order().ToArray();
        var arraytwo = listTwo.Order().ToArray();

        for (int i = 0; i < arrayOne.Count(); i++)
        {
            var number1 = arrayOne[i];
            var number2 = arraytwo[i];

            if (number1 > number2)
            {
                finalTotal += number1 - number2;
            }
            else {
                finalTotal += number2 - number1;
            }
        }

        int total = SimularityScore(arrayOne, arraytwo);
        Console.WriteLine($"The simularity score = {total}");

        Console.WriteLine($"FinalTotal: {finalTotal}");

        Console.ReadLine();
    }

    private static int SimularityScore(int[] listOne, int[] listTwo)
    {
        int total = 0;

        foreach (var x in listOne)
        {
            int itemCount = 0;

            foreach (var y in listTwo)
            {
                if (y == x)
                {
                    itemCount++;
                }
            }

            total += itemCount * x;
        }

        return total;
    }
}