using System.Diagnostics.CodeAnalysis;

namespace FocusOn.Framework.Business.Contract.Http;

/// <summary>
/// 定义让接口具备 HTTP 方式访问的路由形式。该特性要求接口继承 <see cref="IRemotingService"/> 后生效。
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class RouteAttribute : Attribute, IRouteProvider
{
    /// <summary>
    /// 初始化 <see cref="RouteAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">路由模板。</param>
    public RouteAttribute([NotNull] string template)
        => Template = template ?? throw new ArgumentNullException(nameof(template));
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Template { get; }
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Order { get; set; } = 1000;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Name { get; set; }
}
