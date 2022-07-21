using System.Diagnostics.CodeAnalysis;

namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 定义统一的根路由。
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class RouteAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="RouteAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public RouteAttribute([NotNull] string template)
        => Template = template ?? throw new ArgumentNullException(nameof(template));
    /// <summary>
    /// 获取路由模板。
    /// </summary>
    public string Template { get; }
    /// <summary>
    /// 排序。会根据从小到大的排序顺序，获取第一个作为路由。
    /// </summary>
    public int Order { get; set; } = 1000;
}
