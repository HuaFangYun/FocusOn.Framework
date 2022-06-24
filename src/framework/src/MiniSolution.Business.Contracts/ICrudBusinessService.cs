﻿using MiniSolution.Business.Contracts.DTO;

namespace MiniSolution.Business.Contracts;

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TModel">输出、输出模型。</typeparam>
public interface ICrudBusinessService<TKey, TModel>
    :ICrudBusinessService<TKey,TModel, TModel, TModel,TModel>
    where TModel : class
{

}

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateOrUpdateInput">创建或更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : ICrudBusinessService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{

}

/// <summary>
/// 提供具备增删改查的业务服务。
/// </summary>
/// <typeparam name="TKey">主键类型。</typeparam>
/// <typeparam name="TGetOutput">获取单个结果的输出类型。</typeparam>
/// <typeparam name="TGetListOutput">获取列表结果的输出类型。</typeparam>
/// <typeparam name="TGetListInput">获取列表结果的输入类型。</typeparam>
/// <typeparam name="TCreateInput">创建数据的输入类型。</typeparam>
/// <typeparam name="TUpdateInput">更新数据的输入类型。</typeparam>
public interface ICrudBusinessService<TKey,TGetOutput,TGetListOutput,TGetListInput,TCreateInput,TUpdateInput> :IBusinessSerivce
    where TGetListInput:class
    where TGetListOutput:class
    where TGetOutput:class
    where TCreateInput:class
    where TUpdateInput:class
{
    /// <summary>
    /// 以异步的方式创建指定的输入模型对象。
    /// </summary>
    /// <param name="model">要创建的模型。</param>
    /// <returns>一个创建方法，返回 <see cref="OutputResult"/> 结果。</returns>
    ValueTask<OutputResult> CreateAsync(TCreateInput model);
    /// <summary>
    /// 以异步的方式更新指定 id 的输入模型对象。
    /// </summary>
    /// <param name="id">要更新的 id。</param>
    /// <param name="model">要更新的输入模型。</param>
    /// <returns>一个更新方法，返回 <see cref="OutputResult"/> 结果。</returns>
    ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInput model);
    /// <summary>
    /// 以异步的方式删除指定的 id。
    /// </summary>
    /// <param name="id">要删除的 id。</param>
    /// <returns>一个删除方法，返回 <see cref="OutputResult"/> 结果。</returns>
    ValueTask<OutputResult> DeleteAsync(TKey id);
    /// <summary>
    /// 以异步的方式获取指定 id 的结果。
    /// </summary>
    /// <param name="id">要获取的 id 。</param>
    /// <returns>一个获取结果的方法，返回 <see cref="OutputResult{TGetOutput}"/> 结果。</returns>
    ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id);
    /// <summary>
    /// 以异步的方式获取指定分页的结果列表。
    /// </summary>
    /// <param name="model">获取列表的过滤输入模型。</param>
    /// <returns>一个获取分页结果的方法，返回 <see cref="OutputResult{PagedOutputDto{TGetListOutput}}"/> 结果。</returns>
    Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(TGetListInput? model=default);
}
