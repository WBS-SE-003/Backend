public class ConcertTicket : Ticket
{
    public string Band { get; set; } = "";

    public ConcertTicket(string holder, decimal price, string band)
        : base(holder, price)
    {
        Band = band;

    }

    public override void PrintInfo()
    {
        Console.WriteLine($"Concert ticket for {Holder}, Band: {Band}, Price: {Price:C}");
    }
}