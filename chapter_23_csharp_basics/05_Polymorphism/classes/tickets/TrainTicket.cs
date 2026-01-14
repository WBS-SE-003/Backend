public class TrainTicket : Ticket
{
    public string Destination { get; set; }
    public TrainTicket(string holder, decimal price, string destination)
        : base(holder, price)
    {
        Destination = destination;
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Train ticket for {Holder}, Destination: {Destination}, Price: {Price:C}"
        );
    }
}