using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace MiniSolution;

/// <summary>
/// 表示用于构建 MiniSolution 相关模块。
/// </summary>
public class MiniSolutionBuilder
{
    /// <summary>
    /// 初始化 <see cref="MiniSolutionBuilder"/> 类的新实例。
    /// </summary>
    /// <param name="services">注册服务集合。</param>
    internal MiniSolutionBuilder(IServiceCollection services)
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
    public MiniSolutionBuilder AddAutoMapper(params Assembly[] assemblies)
    {
        Services.AddAutoMapper(assemblies);
        return this;
    }
}
