using Utils;

// ETHODS
void SayHello(string name)

{
    Console.WriteLine($"Hello, {name}");
}

string GetGreeting()
{
    return "Hello"; // returns a value
}

// Void method (action only)

void PrintLine()
{
    Console.WriteLine("This is a simple message.");
}

// no input, no return value, just performs an action

// method with parameters

void Greet(string name)
{
    Console.WriteLine($"Hello, {name}");
}

// name input

// Method with return value

int Add(int a, int b)
{
    return a + b;
}

// This method takes input and returns a value.

// return vs console output

int Multiply(int a, int b)
{
    return a * b;
}

// return → sends a value back
// Console.WriteLine → just prints

// Access Modifiers (Visibility)
// [access] [static] returnType MethodName(type parameters)

// PUBLIC

// Main execution
SayHello("Alice");
GetGreeting();
PrintLine();
Greet("Alice");
Greet("Bob");
int result = Add(3, 4);
Console.WriteLine(result); // 7
Console.WriteLine(Multiply(2, 5));

Car toyota = new();
toyota.Drive();
// toyota.StartEngine(); // compile error. Private is visible only inside the same class

// Person jane = new();
// jane.UseCar();

Console.WriteLine(Maths.Square(5));

// comes from Utils.cs
Console.WriteLine(MathsInUtils.Square(5));

// method overloading
Console.WriteLine(Formatter.FormatPrice(12.5));
Console.WriteLine(Formatter.FormatPrice(12.5, "€"));



public class Car
{
    // private (visible only inside the same class)
    private void StartEngine()
    {
        Console.WriteLine("Engine started.");
    }

    public void Drive()
    {
        StartEngine();
        Console.WriteLine("Driving...");
    }
}

public class Person
{
    public void UseCar()
    {
        Car car = new();

        car.Drive();         // ✅ OK
        // car.StartEngine(); // ❌ Compile-time error

    }
}

// Static Methods

public class Maths
{
    public static int Square(int x)
    {
        return x * x;
    }
}


// NAMESPACES (visit Greeter.cs & Utils.cs)

// Method overloading
public class Formatter
{
    public static string FormatPrice(double amount)
    {
        return $"£{amount:0.00}";
    }

    public static string FormatPrice(double amount, string currency)
    {
        return $"{currency}{amount:0.00}";
    }
}
// double is a numeric type for decimal numbers.


// pass by reference with ref

int x = 15;

IncrementByValue(x);
Console.WriteLine(x); // 15

IncrementByRef(ref x);
Console.WriteLine(x); // 16


void IncrementByValue(int number)
{
    number++;
}

void IncrementByRef(ref int number)
{
    number++;
}

// we are giving the method direct access to x variable

// out

double result;
// double is a numeric type for decimal numbers.
// Call the method and receive two outputs:
// - success → tells us if the operation worked
// - result  → contains the division result
bool success = TryDivide(2, 0, out result);

Console.WriteLine(success); // false
Console.WriteLine(result);  // 0


bool TryDivide(int a, int b, out double result)
{
    // Check for invalid division
    if (b == 0)
    {
        result = 0;      // out parameter must be assigned
        return false;    // signal failure
    }

    // Perform division
    result = (double)a / b;
    return true;         // signal success
}

// in

Log("System started");

void Log(in string message)
{
    message = "idontknow"; // This line will break the code because message is a readonly reference
    Console.WriteLine(message);
}


