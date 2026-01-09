namespace Methods;

public class BasicMethods
{
    public static void SayHello(string name)
    {
        Console.WriteLine($"Hello, {name}");
    }

    public static string GetGreeting()
    {
        return "Hello"; // returns a value
    }

    public static void PrintLine()
    {
        Console.WriteLine("This is a simple message.");
    }

    public static void Greet(string name)
    {
        Console.WriteLine($"Hello, {name}");
    }

    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }
}
