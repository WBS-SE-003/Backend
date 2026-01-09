Greeter.Welcome(); // uses both defaults
Greeter.Welcome(course: "LINQ"); // named argument
Greeter.Welcome(name: "Toni"); // override
Greeter.Welcome("Josh", "ASP.NET"); // positional

public class Greeter // global namespace
{
    public static void Welcome(string name = "student", string course = "C# Basics")
    {
        Console.WriteLine($"Welcome {name} to {course}");
    }
}

// Default parameters are fallback values
// we can always override them.
