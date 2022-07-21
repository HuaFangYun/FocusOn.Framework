namespace FocusOn.Framework.Business.Contract;

/// <summary>
/// 提供业务服务的功能。
/// </summary>
public interface IBusinessSerivce
{
    /// <summary>
    /// 获取注入的服务提供者。
    /// </summary>
    IServiceProvider ServiceProvider { get; }
}
