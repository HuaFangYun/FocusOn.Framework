namespace FocusOn.Framework;
/// <summary>
/// 对象的扩展。
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// 将当前对象转换为指定的 <typeparamref name="T"/> 类型。
    /// </summary>
    /// <typeparam name="T">要转换的类型。</typeparam>
    /// <param name="obj">当前对象。</param>
    /// <returns></returns>
    public static T As<T>(this object obj) => (T)obj;
}
