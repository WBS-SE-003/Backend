public class PointTests
{
    [Fact]
    public void Records_CompareByValue_ButReferencesDiffer()
    {
        // Arrange
        // Two different objects with the same values
        var a = new Point(1, 2);
        var b = new Point(1, 2);

        // Records use value equality:
        // same X and Y => considered equal
        Assert.Equal(a, b);

        // But they are still two different objects in memory
        Assert.NotSame(a, b);

        // Act
        // c now points to the same object as a
        var c = a;

        // Same reference => same object
        Assert.Same(a, c);
    }
}