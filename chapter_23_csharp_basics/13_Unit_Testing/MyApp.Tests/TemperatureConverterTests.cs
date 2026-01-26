
using MyApp;
public class TemperatureConverterTests
{
    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    public void CelsiusToFahrenheit_KnownPoints(double c, double f)
        // These are exact, well-known values
        // So we can use Assert.Equal
        => Assert.Equal(
            f,
            TemperatureConverter.CelsiusToFahrenheit(c),
            precision: 5
        // precision allows very small decimal differences in double results
        // like 3.9999999 or 4.0000001 instead of exactly 4.0
        // precision: 5 compares the result up to 5 decimal places

        );

    [Fact]
    public void CelsiusToFahrenheit_InRange()
    {
        // Arrange
        // 20°C does not have a "special" exact value
        var f = TemperatureConverter.CelsiusToFahrenheit(20);

        // Assert
        // We only check that the result is close to 68°F
        Assert.InRange(f, 67.9, 68.1);
    }

    [Fact]
    public void StringAsserts_Examples()
    {
        // Arrange
        var msg = "Hello, world!";

        // Assert
        // Simple string checks
        Assert.StartsWith("Hello", msg);
        Assert.EndsWith("!", msg);
        Assert.NotNull(msg);
    }
}