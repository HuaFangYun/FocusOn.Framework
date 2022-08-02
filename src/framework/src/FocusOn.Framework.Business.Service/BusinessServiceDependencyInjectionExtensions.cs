using FocusOn.Framework;
using FocusOn.Framework.Business.Service.Identity;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="FocusOnBuilder"/> 的扩展。
/// </summary>
public static class BusinessServiceDependencyInjectionExtensions
{
    /// <summary>
    /// 向 <see cref="FocusOnBuilder"/> 添加契约与业务的服务。
    /// </summary>
    /// <typeparam name="TContractService">契约类型。</typeparam>
    /// <typeparam name="TBusinessService">业务服务类型。</typeparam>
    /// <param name="builder"><see cref="FocusOnBuilder"/> 实例。</param>
    public static FocusOnBuilder AddBusinessService<TContractService, TBusinessService>(this FocusOnBuilder builder)
        where TContractService : class
        where TBusinessService : class, TContractService
    {
        builder.Services.AddScoped<TContractService, TBusinessService>();
        return builder;
    }

    /// <summary>
    /// 添加 Identity 模块。
    /// </summary>
    /// <param name = "builder" ><see cref="FocusOnBuilder"/> 实例。</param>
    public static IdentityFocusOnBuilder AddIdentity(this FocusOnBuilder builder)
        => new(builder);
}
