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
}