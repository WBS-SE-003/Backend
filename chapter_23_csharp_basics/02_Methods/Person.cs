namespace Methods;

public class Person
{
    public void UseCar()
    {
        Car car = new();

        car.Drive();         // ✅ OK
        // car.StartEngine(); // ❌ Compile-time error

    }
}
