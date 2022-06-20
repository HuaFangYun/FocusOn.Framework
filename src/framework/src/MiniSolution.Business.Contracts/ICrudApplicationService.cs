using MiniSolution.Business.Contracts.DTO;

namespace MiniSolution.Business.Contracts;
public interface ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput>
    : ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateOrUpdateInput, TCreateOrUpdateInput>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateOrUpdateInput : class
{

}

public interface ICrudApplicationService<TKey,TGetOutput,TGetListOutput,TGetListInput,TCreateInput,TUpdateInput> :IApplicationSerivce
    where TGetListInput:class
    where TGetListOutput:class
    where TGetOutput:class
    where TCreateInput:class
    where TUpdateInput:class
{
    ValueTask<OutputResult> CreateAsync(TCreateInput model);
    ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInput model);
    ValueTask<OutputResult> DeleteAsync(TKey id);
    ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id);
    Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(int page, int size, TGetListInput? model=default);
}
