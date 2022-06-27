
using Microsoft.AspNetCore.Mvc;

using FocusOn.Business.Contracts;
using Microsoft.Extensions.DependencyInjection;
using FocusOn.Business.Contracts.DTO;

namespace FocusOn.Endpoints.HttpApi.Controllers;


/// <summary>
/// 表示具备 CRUD 功能的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TBusinessService">业务服务类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public abstract class CrudApiControllerBase<TBusinessService, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : CrudApiControllerBase<TBusinessService, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TBusinessService : ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    where TGetOutput : class
    where TGetListInput : class
    where TGetListOutput : class
    where TCreateOrUpdateInput : class
{

}

/// <summary>
/// 表示具备 CRUD 功能的 HTTP API 控制器基类。
/// </summary>
/// <typeparam name="TBusinessService">业务服务类型。</typeparam>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
[Produces("application/json")]
public abstract class CrudApiControllerBase<TBusinessService, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    : ApiControllerBase, ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TBusinessService : ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetOutput : class
    where TGetListInput : class
    where TGetListOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    /// <summary>
    /// 获取已注册服务的 <typeparamref name="TBusinessService"/> 类型.
    /// </summary>
    protected virtual TBusinessService BusinessService => ServiceProvider.GetRequiredService<TBusinessService>();

    /// <summary>
    /// 创建指定 <typeparamref name="TCreateInput"/> 的数据。
    /// </summary>
    /// <param name="model">要创建的输入模型。</param>
    [HttpPost]
    [ProducesResponseType(200,Type =typeof(OutputResult))]
    public virtual ValueTask<OutputResult> CreateAsync([FromBody]TCreateInput model)
    => BusinessService.CreateAsync(model);

    /// <summary>
    /// 删除指定 id 的数据。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200, Type = typeof(OutputResult))]
    public virtual ValueTask<OutputResult> DeleteAsync(TKey id)
    => BusinessService.DeleteAsync(id);

    /// <summary>
    /// 获取指定 id 的数据。
    /// </summary>
    /// <param name="id">要获取的 id。</param>
    [ProducesResponseType(200, Type = typeof(OutputResult<>))]
    [HttpGet("{id}")]
    public virtual ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    => BusinessService.GetAsync(id);

    /// <summary>
    /// 获取指定 <typeparamref name="TGetListInput"/> 的列表。
    /// </summary>
    /// <param name="model">列表的过滤输入模型。</param>
    [ProducesResponseType(200, Type = typeof(OutputResult<>))]
    [HttpGet]
    public virtual Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync([FromQuery] TGetListInput? model = null)
    => BusinessService.GetListAsync(model);

    /// <summary>
    /// 更新指定 id 的数据。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的输入模型。</param>
    [ProducesResponseType(200, Type = typeof(OutputResult))]
    [HttpPut("{id}")]
    public virtual ValueTask<OutputResult> UpdateAsync(TKey id, [FromBody]TUpdateInput model)
    => BusinessService.UpdateAsync(id, model);
}
