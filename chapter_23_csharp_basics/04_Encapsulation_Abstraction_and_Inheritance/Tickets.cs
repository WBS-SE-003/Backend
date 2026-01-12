public class Ticket
{
    public string? Holder { get; set; }
    public decimal Price { get; set; }

    public Ticket(string holder, decimal price)
    {
        Holder = holder;
        Price = price;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Ticket for {Holder}, Price: {Price}");
    }
}