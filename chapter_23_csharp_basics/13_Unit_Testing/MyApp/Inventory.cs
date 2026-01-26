public sealed class Inventory
{
    // Internal list that stores the items
    private readonly List<string> _items = new();

    // Add a new item to the inventory
    public void Add(string item) => _items.Add(item);

    // Remove returns true if the item was found and removed
    public bool Remove(string item) => _items.Remove(item);

    // Expose items as read-only to protect internal state
    public IReadOnlyList<string> GetAll() => _items;
}