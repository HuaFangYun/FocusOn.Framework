using System.Reflection;

namespace MiniSolution;

public static class ReflectionHelper
{
    public static bool TryGetCustomAttribute<TAttribute>(this Type type, out TAttribute? attribute) where TAttribute : Attribute
    {
        attribute = type.GetCustomAttribute<TAttribute>();
        return attribute != null;
    }
}
