using Methods;
using Utils;

// --- Basic Methods ---
BasicMethods.SayHello("Alice");
BasicMethods.GetGreeting();
BasicMethods.PrintLine();
BasicMethods.Greet("Alice");
BasicMethods.Greet("Bob");

int result = BasicMethods.Add(3, 4);
Console.WriteLine(result); // 7
Console.WriteLine(BasicMethods.Multiply(2, 5));

// --- Classes and Objects ---
Car toyota = new();
toyota.Drive();
// toyota.StartEngine(); // compile error. Private is visible only inside the same class

// Person usage
Person jane = new();
jane.UseCar();

// --- Static Methods ---
Console.WriteLine(Maths.Square(5));

// comes from Utils.cs
Console.WriteLine(MathsInUtils.Square(5));

// --- Method Overloading ---
Console.WriteLine(Formatter.FormatPrice(12.5));
Console.WriteLine(Formatter.FormatPrice(12.5, "€"));

// --- Optional Parameters (Greeter) ---
Greeter.Welcome(); // uses both defaults
Greeter.Welcome(course: "LINQ"); // named argument
Greeter.Welcome(name: "Toni"); // override
Greeter.Welcome("Josh", "ASP.NET"); // positional

// --- Parameter Modifiers (ref, out, in) ---

// pass by reference with ref
int x = 15;

ParameterModifiers.IncrementByValue(x);
Console.WriteLine($"IncrementByValue: {x}"); // 15

ParameterModifiers.IncrementByRef(ref x);
Console.WriteLine($"IncrementByRef: {x}"); // 16

// out parameter
double divisionResult;
// Call the method and receive two outputs:
// - success → tells us if the operation worked
// - result  → contains the division result
bool success = ParameterModifiers.TryDivide(2, 0, out divisionResult);

Console.WriteLine($"TryDivide Success: {success}"); // false
Console.WriteLine($"TryDivide Result: {divisionResult}");  // 0

// in parameter
ParameterModifiers.Log("System started");


