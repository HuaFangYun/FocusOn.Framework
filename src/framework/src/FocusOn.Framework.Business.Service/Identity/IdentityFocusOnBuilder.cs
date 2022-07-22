using Microsoft.Extensions.DependencyInjection;

namespace FocusOn.Framework.Business.Services.Identity;

/// <summary>
/// 表示 Identity 模块的构建。
/// </summary>
public class IdentityFocusOnBuilder
{
    /// <summary>
    /// 初始化 <see cref="IdentityFocusOnBuilder"/> 类的新实例。
    /// </summary>
    /// <param name="builder"><see cref="FocusOnBuilder"/> 实例。</param>
    internal IdentityFocusOnBuilder(FocusOnBuilder builder)
    {
        Builder = builder;

        AddDefaultHashPasswordService();
    }

    /// <summary>
    /// 获取 <see cref="FocusOnBuilder"/> 实例。
    /// </summary>
    public FocusOnBuilder Builder { get; }

    /// <summary>
    /// 添加密码哈希服务。
    /// </summary>
    /// <typeparam name="TService">哈希密码算法服务类型。</typeparam>
    public IdentityFocusOnBuilder AddHashPasswordService<TService>()
        where TService : class, IHashPasswordService
    {
        Builder.Services.AddSingleton<IHashPasswordService, TService>();
        return this;
    }

    /// <summary>
    /// 添加默认的 MD5 密码哈希服务。
    /// </summary>
    public IdentityFocusOnBuilder AddDefaultHashPasswordService()
        => AddHashPasswordService<DefaultPasswordService>();
}
