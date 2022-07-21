namespace FocusOn.Framework.Business.Contract.Http;
/// <summary>
/// 定义请求参数从查询语句中获取。
/// </summary>
public class QueryAttribute : HttpParameterAttribute
{
    /// <summary>
    /// 初始化 <see cref="QueryAttribute"/> 类的新实例。
    /// </summary>
    /// <param name="name">参数名称的重命名。<c>null</c> 表示使用参数本身的名称。</param>
    public QueryAttribute(string? name = default) : base(HttpParameterType.FromQuery, name)
    {

    }
}
