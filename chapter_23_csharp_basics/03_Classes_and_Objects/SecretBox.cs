public class SecretBox
{
    private string _code = "0000";

    public string Code
    {
        get
        {
            return _code;
        }

        set
        {
            if (value.Length != 4)
            {
                Console.WriteLine("Invalid code");
                return;
            }

            _code = value;
        }
    }
}