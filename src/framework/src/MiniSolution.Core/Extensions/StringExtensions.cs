namespace MiniSolution;

/// <summary>
/// 字符串扩展类。
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 判断当前字符串是 <c>null</c> 还是空字符串。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <returns>若为 <c>null</c> 或空字符串则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsNullOrEmpty(this string value)
        => string.IsNullOrEmpty(value);

    /// <summary>
    /// 判断当前字符串是 <c>null</c> 、空字符串或空白字符串。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <returns>若为 <c>null</c> 、空字符串或空白字符串则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 使用指定数组的参数值替换当前字符串的格式化占位字符。
    /// </summary>
    /// <param name="value">当前字符串。</param>
    /// <param name="args">要替换格式化占位的值。</param>
    /// <returns>替换后的字符串。</returns>
    public static string StringFormat(this string value, params object[] args)
        => string.Format(value, args);

    /// <summary>
    /// 使用指定的分隔符，将当前数组对象进行=连接。
    /// </summary>
    /// <param name="value">要连接的对象数组。</param>
    /// <param name="seperator">分隔符字符串。</param>
    /// <returns>数组使用分隔符进行连接过后的字符串。</returns>
    public static string Join(this object[] value, string seperator)
        => string.Join(seperator, value);
}
