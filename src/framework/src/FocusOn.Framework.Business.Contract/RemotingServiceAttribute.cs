namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 表示业务的接口、类、方法支持远程服务代理的功能。
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
public sealed class RemotingServiceAttribute : Attribute
{
    /// <summary>
    /// 初始化 <see cref="RemotingServiceAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="template">远程服务的路由。</param>
    public RemotingServiceAttribute(string? template = default)
    {
        Template = template;
    }
    /// <summary>
    /// 获取路由模板字符串。
    /// </summary>
    public string Template { get; }
    /// <summary>
    /// 获取或设置一个布尔值，表示禁用远程服务代理。
    /// </summary>
    public bool Disabled { get; set; }
}
