public class Animal
{
    public string Name { get; set; } = "";

    public Animal(string name)
    {
        Name = name;
    }

    public void Eat()
    {
        Console.WriteLine($"{Name} is eating");
    }

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping");
    }
}
