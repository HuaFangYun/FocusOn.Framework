using System.Reflection;

namespace MiniSolution;

/// <summary>
/// 反射的扩展类。
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// 尝试从当前的 <see cref="Type"/> 类型返回 <typeparamref name="TAttribute"/> 类型。
    /// </summary>
    /// <typeparam name="TAttribute">属性类型。</typeparam>
    /// <param name="type"><see cref="Type"/> 的实例。</param>
    /// <param name="attribute">返回获取到的 <typeparamref name="TAttribute"/> 类型。</param>
    /// <returns>若能获取到指定的 <typeparamref name="TAttribute"/> 类型，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool TryGetCustomAttribute<TAttribute>(this Type type, out TAttribute? attribute) where TAttribute : Attribute
    {
        attribute = type.GetCustomAttribute<TAttribute>();
        return attribute != null;
    }
}
