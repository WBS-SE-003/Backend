var animals = new List<Animal>
{
    new Dog("Rocky"),
    new Cat("Tata")
};


foreach (var animal in animals)
{
    animal.MakeSound();
}

var tickets = new List<Ticket>
{
    new TrainTicket("Mohamed", 50, "Berlin"),
    new ConcertTicket("Lukasz", 50, "DJ Tiesto")
};

foreach (var ticket in tickets)
{
    ticket.PrintInfo();
}