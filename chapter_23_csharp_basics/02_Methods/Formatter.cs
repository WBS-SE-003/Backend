namespace Methods;

// Method overloading
public class Formatter
{
    public static string FormatPrice(double amount)
    {
        return $"£{amount:0.00}";
    }

    public static string FormatPrice(double amount, string currency)
    {
        return $"{currency}{amount:0.00}";
    }
}
