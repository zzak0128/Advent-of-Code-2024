internal class Program
{
    private static void Main(string[] args)
    {
        FileInfo inputFile = new(args[0]);
        using (var reader = new StreamReader(inputFile.FullName))
        {
            while (!reader.EndOfStream)
            {
                Console.WriteLine(reader.ReadLine());
            }
        }
    }
}