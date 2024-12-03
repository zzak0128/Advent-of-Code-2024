internal class Program
{
    private static void Main(string[] args)
    {
        FileInfo inputFile = new(args[0]);
        //FileInfo inputFile = new(@"/home/zzak/Downloads/DayOneInput");
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

        Console.WriteLine($"FinalTotal: {finalTotal}");

        Console.ReadLine();
    }
}