using MyApp;
using Xunit;

public class SimpleCalculatorTests
{
    [Fact]
    public void Divide_ThrowsException_WhenDividingByZero()
    {
        var calc = new SimpleCalculator();

        // Assert.Throws:
        // This test passes ONLY if an exception is thrown
        Assert.Throws<ArgumentException>(
            () => calc.Divide(10, 0)
        );
    }

    [Fact]
    public void Divide_ReturnsResult_WhenInputIsValid()
    {
        var calc = new SimpleCalculator();

        int result = calc.Divide(10, 2);

        // Normal result check
        Assert.Equal(5, result);
    }
}