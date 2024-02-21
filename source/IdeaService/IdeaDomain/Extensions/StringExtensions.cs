namespace IdeaDomain.Extensions;

public static class StringExtensions
{
    public static string CheckIdeaForBadWords(this string text)
    {
        if (text.Contains("bad words"))
            return string.Empty;

        return text;
    }
    
}