public class Car // global namespace
{
    // data (property)
    public string Brand { get; set; } = "";
    private void StartEngine()
    {
        Console.WriteLine("Engine has started...");
    }

    public void Drive()
    {
        StartEngine();
        Console.WriteLine($"{Brand} is driving...");
    }
}