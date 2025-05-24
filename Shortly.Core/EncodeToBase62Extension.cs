namespace Shortly.Core;

public static class EncodeToBase62Extension
{
    private const string AlphaNum = "0123456789" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz";

    public static string EncodeToBase62(this long number)
    {
        if (number == 0) return AlphaNum[0].ToString();
        var result = new Stack<char>();
        while (number > 0)
        {
            result.Push(AlphaNum[(int)(number % 62)]);
            number /= 62;
        }
        return new string(result.ToArray());
    }
}