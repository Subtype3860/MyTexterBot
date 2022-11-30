namespace MyTexterBot.Extensions;

public static class StringExtension
{
    public static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return String.Empty;
        }

        return char.ToUpper(s[0]) + s.Substring(1);
    }
}