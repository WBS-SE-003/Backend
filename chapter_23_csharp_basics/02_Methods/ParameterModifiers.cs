namespace Methods;

public class ParameterModifiers
{
    public static void IncrementByValue(int number)
    {
        number++;
    }

    public static void IncrementByRef(ref int number)
    {
        number++;
    }

    public static bool TryDivide(int a, int b, out double result)
    {
        // Check for invalid division
        if (b == 0)
        {
            result = 0;      // out parameter must be assigned
            return false;    // signal failure
        }

        // Perform division
        result = (double)a / b;
        return true;         // signal success
    }

    public static void Log(in string message)
    {
        // message = "idontknow"; // This line will break the code because message is a readonly reference
        Console.WriteLine(message);
    }
}
