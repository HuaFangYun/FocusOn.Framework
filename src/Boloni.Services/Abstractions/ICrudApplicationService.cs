using AutoMapper;

using Boloni.Data;
using Boloni.Data.Entities;

namespace Boloni.Services.Abstractions;
public interface ICrudApplicationService<TEntity, TKey>
    : ICrudApplicationService<TEntity, TKey, TEntity, TEntity, TEntity>
    where TEntity : EntityBase<TKey>
{

}

public interface ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput>
    : ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TEntity>
    where TEntity : EntityBase<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
{

}

public interface ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : ICrudApplicationService<TEntity, TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TEntity : EntityBase<TKey>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{

}

public interface ICrudApplicationService<TEntity,TKey,TGetOutput,TGetListOutput,TGetListInput,TCreateInput,TUpdateInput> 
    where TEntity : EntityBase<TKey>
    where TGetListInput:class
    where TGetListOutput:class
    where TGetOutput:class
    where TCreateInput:class
    where TUpdateInput:class
{
    ValueTask<ApplicationResult> CreateAsync(TCreateInput model);
    ValueTask<ApplicationResult> UpdateAsync(TKey id, TUpdateInput model);
    ValueTask<ApplicationResult> DeleteAsync(TKey id);
    ValueTask<TGetOutput> GetAsync(TKey id);
    Task<IReadOnlyList<TGetListOutput>> GetListAsync(TGetListInput model);
    Task<(IReadOnlyList<TGetListOutput> Data, long Total)> GetPagedListAsync(int page, int size, TGetListInput model=default);
}
