using System.Runtime.CompilerServices;
using System.Transactions;

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

        var wordGrid = CreateGridArray(lines);
        var wordList = WordSearchSurrounding(wordGrid);

        int matchingWordCount = 0;
        foreach (var word in wordList)
        {
            if (IsValidWord(word, "XMAS"))
            {
                matchingWordCount++;
            }
        }

        System.Console.WriteLine($"The total count of found 'XMAS' is {matchingWordCount}");

        // Part Two
        int matchingCrosses = WordSearchCross(wordGrid);
        System.Console.WriteLine($"The total count of X-MASes found is {matchingCrosses}");
    }

    private static char[,] CreateGridArray(List<string> lines)
    {
        //char[,] grid
        var columns = lines.Max(x => x.Length);
        var rows = lines.Count;
        char[,] grid = new char[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            char[] array = lines[i].ToCharArray();

            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = array[j];
            }
        }

        return grid;
    }

    private static List<string> WordSearchSurrounding(char[,] wordGrid)
    {
        List<string> wordList = [];

        for (int i = 0; i < wordGrid.GetLength(0); i++)
        {
            for (int j = 0; j < wordGrid.GetLength(1); j++)
            {
                foreach (var direction in Enum.GetValues(typeof(Direction)))
                {
                    wordList.Add(CheckDirection(i, j, wordGrid, (Direction)direction));
                }
            }
        }

        return wordList;
    }

    private static int WordSearchCross(char[,] wordGrid)
    {
        int validCrosses = 0;

        for (int i = 0; i < wordGrid.GetLength(0); i++)
        {
            for (int j = 0; j < wordGrid.GetLength(1); j++)
            {
                string diagnalOne = "";
                string diagnalTwo = "";

                diagnalOne += CheckDirection(i, j, wordGrid, Direction.DownLeft, 1, true);
                diagnalOne += wordGrid[i, j];
                diagnalOne += CheckDirection(i, j, wordGrid, Direction.UpRight, 1, true);

                diagnalTwo += CheckDirection(i, j, wordGrid, Direction.UpLeft, 1, true);
                diagnalTwo += wordGrid[i, j];
                diagnalTwo += CheckDirection(i, j, wordGrid, Direction.DownRight, 1, true);

                bool isDiagnalOneValid = IsValidWord(diagnalOne, "MAS") || IsValidWord(diagnalOne, "SAM");
                bool isDiagnalTwoValid = IsValidWord(diagnalTwo, "MAS") || IsValidWord(diagnalTwo, "SAM");

                if (isDiagnalOneValid && isDiagnalTwoValid)
                {
                    validCrosses++;
                }
            }
        }

        return validCrosses;
    }

    private static string CheckDirection(int row, int col, char[,] wordGrid, Direction direction, int wordLength = 4, bool skipFirst = false)
    {
        int[] IncreaseBy = new int[2];
        switch (direction)
        {
            case Direction.Up:
                IncreaseBy[0] = 0;
                IncreaseBy[1] = -1;
                break;
            case Direction.UpRight:
                IncreaseBy[0] = 1;
                IncreaseBy[1] = -1;
                break;
            case Direction.Right:
                IncreaseBy[0] = 1;
                IncreaseBy[1] = 0;
                break;
            case Direction.DownRight:
                IncreaseBy[0] = 1;
                IncreaseBy[1] = 1;
                break;
            case Direction.Down:
                IncreaseBy[0] = 0;
                IncreaseBy[1] = 1;
                break;
            case Direction.DownLeft:
                IncreaseBy[0] = -1;
                IncreaseBy[1] = 1;
                break;
            case Direction.Left:
                IncreaseBy[0] = -1;
                IncreaseBy[1] = 0;
                break;
            case Direction.UpLeft:
                IncreaseBy[0] = -1;
                IncreaseBy[1] = -1;
                break;
            default:
                IncreaseBy[0] = 0;
                IncreaseBy[1] = 0;
                break;
        }

        var currentRow = row;
        var currentCol = col;
        int charCheckCount = 0;

        string foundWord = "";
        bool skip = skipFirst;

        do
        {
            if (currentCol < 0 || currentCol >= wordGrid.GetLength(1))
            {
                return "";
            }
            if (currentRow < 0 || currentRow >= wordGrid.GetLength(0))
            {
                return "";
            }

            if (!skip)
            {
                foundWord += wordGrid[currentRow, currentCol];
            }
            else
            {
                skip = false;
                charCheckCount -= 1;
            }

            currentRow += IncreaseBy[0];
            currentCol += IncreaseBy[1];
            charCheckCount++;

        } while (charCheckCount < wordLength);

        return foundWord;
    }

    private static bool IsValidWord(string word, string validWord)
    {
        if (word.Contains(validWord))
        {
            return true;
        }

        return false;
    }
}



public enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}