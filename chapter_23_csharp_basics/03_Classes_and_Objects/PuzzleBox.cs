public class PuzzleBox
{
    private string _secret = "Empty";

    public PuzzleBox(string owner, string secret)
    {
        Owner = owner;
        Secret = secret;
    }

    public string Owner { get; set; } = "";
    public string Secret
    {
        get
        {
            return _secret;
        }
        set
        {
            _secret = value;
        }
    }

    public void Open()
    {
        Console.WriteLine($"{Owner} opens the box and finds: {Secret}");
    }

}