0️⃣ Project Structure

## 🏗️ Project Structure (Very Important)

In .NET, tests live in a **separate project**.

```
MySolution/
  MyApp/              ← application code
    Calculator.cs
  MyApp.Tests/        ← unit tests
    CalculatorTests.cs
  MySolution.sln
```

## 🛠️ Creating the Structure

```bash
mkdir -p MySolution && cd MySolution

dotnet new console -o MyApp
# Creates a new console application project named "MyApp"
# This is where our application (production) code lives

dotnet new xunit -o MyApp.Tests
# Creates a new xUnit test project named "MyApp.Tests"
# This project will contain all unit tests for MyApp

dotnet new sln -n MySolution
# Creates a solution file (MySolution.sln)
# A solution groups multiple related projects together


dotnet sln add MyApp/MyApp.csproj
# Adds the MyApp project to the solution
# The solution now knows about the application project

dotnet sln add MyApp.Tests/MyApp.Tests.csproj
# Adds the test project to the solution
# Both projects are now managed under the same solution


dotnet add MyApp.Tests/MyApp.Tests.csproj reference MyApp/MyApp.csproj
# Adds a project reference from the test project to the app project
# This allows tests to access MyApp classes (Calculator, services, etc.)
# Without this, the test project cannot "see" the application code
```

## ▶️ Running Tests

From the solution root:

```bash
dotnet test
```

✔ Builds the app  
✔ Runs all tests  
✔ Reports success or failure

---

# xUnit Basics

### `[Fact]`

- single test
- no parameters

```csharp
[Fact]
public void Example_Test()
{
    Assert.True(5 > 3);
}
```

---

### `[Theory]` + `[InlineData]`

- same test, multiple inputs

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 5, 5)]
[InlineData(-1, 1, 0)]
public void Add_WorksForManyCases(int a, int b, int expected)
{
    var calc = new Calculator();
    Assert.Equal(expected, calc.Add(a, b));
}
```

---

## Example

### Code we want to test

📄 _MyApp/Calculator.cs_

```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
```

### Test for that code

📄 _MyApp.Tests/CalculatorTests.cs_

```csharp
using Xunit;
using MyApp;

public class CalculatorTests
{
    [Fact] // this marks a unit test
    public void Add_ReturnsCorrectSum()
    {
        var calc = new Calculator();

        int result = calc.Add(2, 3);

        Assert.Equal(5, result);
        // Assert.Equal checks that the actual result
        // matches the expected value
        // If they are not equal, the test fails
    }
}
```

### [Theory]

Fact is one test. Theory is the same test repeated with different data.

```csharp
  [Theory] // Theory = same test logic, multiple inputs
    [InlineData(2, 3, 5)]   // a = 2, b = 3, expected result = 5
    [InlineData(0, 5, 5)]   // a = 0, b = 5, expected result = 5
    [InlineData(-1, 5, 4)]  // a = -1, b = 5, expected result = 4
    public void Add_WorksForMultipleCases(int a, int b, int expected)
    {
        // a and b come from InlineData
        // expected is the result we expect from Add(a, b)
        var calc = new Calculator();

        int result = calc.Add(a, b);

        // 1. Compare the actual result with the expected value
        Assert.Equal(expected, result);

        // 2. Assert.True → checks a condition is true
        Assert.True(result == expected);
        // Here we don’t compare values directly,
        // we check if the condition is true.


        // Assert.Throws → expect an error
    }

```

---

## Common Assertions

### Equality & Boolean

```csharp
Assert.Equal(5, result);
Assert.NotEqual(0, result);
Assert.True(result > 0);
Assert.False(result < 0);
```
