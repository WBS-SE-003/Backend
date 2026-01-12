public class Dog : Animal
{
    public Dog(string name) : base(name)
    {

    }

    public void Bark()
    {
        Console.WriteLine($"{Name} says: woof!");
    }
}