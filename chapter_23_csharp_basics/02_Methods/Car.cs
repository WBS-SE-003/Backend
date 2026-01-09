namespace Methods;

public class Car
{
    // private (visible only inside the same class)
    private void StartEngine()
    {
        Console.WriteLine("Engine started.");
    }

    public void Drive()
    {
        StartEngine();
        Console.WriteLine("Driving...");
    }
}
