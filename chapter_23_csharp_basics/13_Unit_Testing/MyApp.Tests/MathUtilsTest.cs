using MyApp;
using Xunit;

public class MathUtilsTests
{
    [Fact]
    public void Average_Throws_OnNullOrEmpty()
    {
        // Assert.Throws => test PASSES only if the exception is thrown

        // Null input => ArgumentNullException expected
        Assert.Throws<ArgumentNullException>(
            () => MathUtils.Average(null!)
        );

        // Empty list => ArgumentException expected
        Assert.Throws<ArgumentException>(
            () => MathUtils.Average(Array.Empty<int>())
        );
    }

    [Fact]
    public void Average_ReturnsExpected()
    {
        // Arrange
        // We provide a valid collection of numbers
        // This time, no exception should be thrown
        var numbers = new[] { 2, 4, 6 };


        // Act
        // The method calculates the average:
        // (2 + 4 + 6) / 3 = 4.0
        var result = MathUtils.Average(numbers);

        // Assert
        // We expect the average to be 4.0
        // precision: 5 allows small floating-point differences
        Assert.Equal(4.0, result, precision: 5);
        // precision allows very small decimal differences in double results
        // like 3.9999999 or 4.0000001 instead of exactly 4.0
        // precision: 5 compares the result up to 5 decimal places

    }
}