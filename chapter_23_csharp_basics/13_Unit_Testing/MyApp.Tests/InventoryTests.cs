public class InventoryTests
{
    [Fact]
    public void AddAndGetAll_ContainsItems_InOrder()
    {
        // Arrange
        var inv = new Inventory();
        inv.Add("apple");
        inv.Add("banana");

        // Act
        var all = inv.GetAll();

        // Assert
        Assert.Equal(2, all.Count);                  // correct count
        Assert.Contains("apple", all);               // item exists
        Assert.DoesNotContain("cherry", all);        // item does not exist

        // Assert order of elements
        Assert.Collection(all,
            first => Assert.Equal("apple", first),
            second => Assert.Equal("banana", second));
    }
}