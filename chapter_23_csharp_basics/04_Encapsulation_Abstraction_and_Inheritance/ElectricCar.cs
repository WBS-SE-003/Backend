public class ElectricCar : Car
{

    public override void Drive()
    {
        Console.WriteLine("overridden");
    }
    public void Charge()
    {
        Console.WriteLine("The car is charging");
    }
}