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
    }
}