namespace DayFive;

public class ParsedUpdates
{
    public Dictionary<int, List<int>> SortOrders = [];

    public List<List<int>> UpdateLines = [];

}

public enum ReturnType
{
    Valid,
    Invalid
}
