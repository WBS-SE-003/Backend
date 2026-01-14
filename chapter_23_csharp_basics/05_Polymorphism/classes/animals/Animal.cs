public class Animal
{
    public string Name { get; set; } = "";

    public Animal(string name)
    {
        Name = name;
    }

    public virtual void MakeSound()
    {
        Console.WriteLine("The animal makes a sound");
    }
}