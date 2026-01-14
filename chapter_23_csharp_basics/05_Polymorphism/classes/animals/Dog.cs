public class Dog : Animal
{
    public Dog(string name) : base(name)
    { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}