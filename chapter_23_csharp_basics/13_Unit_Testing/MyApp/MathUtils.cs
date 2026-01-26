namespace MyApp;

public static class MathUtils
{
    public static double Average(IEnumerable<int> values)
    {
        // If values is null => throw ArgumentNullException
        // If values is NOT null => convert it to a list
        var list = values?.ToList()
                   ?? throw new ArgumentNullException(nameof(values));

        // If list is empty => throw ArgumentException
        if (list.Count == 0)
            throw new ArgumentException("Sequence is empty", nameof(values));

        // Otherwise => return the average
        return list.Average();
    }
}