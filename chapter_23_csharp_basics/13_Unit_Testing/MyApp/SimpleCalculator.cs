namespace MyApp;

public class SimpleCalculator
{
    public int Divide(int a, int b)
    {
        // Division by zero is not allowed
        if (b == 0)
        {
            throw new ArgumentException("Cannot divide by zero");
        }

        return a / b;
    }
}