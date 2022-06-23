namespace MiniSolution;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
        => string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);

    public static string StringFormat(this string value, params object[] args)
        => string.Format(value, args);

    public static string Join(this object[] value, string seperator)
        => string.Join(seperator, value);
}
