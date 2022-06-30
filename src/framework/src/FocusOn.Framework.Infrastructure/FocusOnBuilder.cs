using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace FocusOn;

/// <summary>
/// 表示用于构建 FocusOn 相关模块。
/// </summary>
public class FocusOnBuilder
{
    /// <summary>
    /// 初始化 <see cref="FocusOnBuilder"/> 类的新实例。
    /// </summary>
    /// <param name="services">注册服务集合。</param>
    internal FocusOnBuilder(IServiceCollection services)
    {
        Services = services;
    }
    /// <summary>
    /// 获取注册服务集合。
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// 添加指定程序集中实现 <see cref="AutoMapper.Profile"/> 类型的映射关系并注册到服务中。
    /// </summary>
    /// <param name="assemblies">要添加的程序集。</param>
    public FocusOnBuilder AddAutoMapper(params Assembly[] assemblies)
    {
        Services.AddAutoMapper(assemblies);
        return this;
    }
}
