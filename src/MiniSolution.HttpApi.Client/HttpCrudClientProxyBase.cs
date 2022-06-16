using System.Net.Http.Json;
using System.Text.Json;

using MiniSolution.ApplicationContracts;
using MiniSolution.ApplicationContracts.DTO;
using MiniSolution.Client.Http;

namespace MiniSolution.HttpApi.Client;

public abstract class HttpCrudClientProxyBase<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : ClientProxyBase, ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : HttpInputDtoBase
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : HttpInputDtoBase
    where TUpdateInput : HttpInputDtoBase
{
    protected HttpCrudClientProxyBase(IServiceProvider services) : base(services)
    {
    }

    protected abstract string RootPath { get; }

    public virtual async ValueTask<OutputResult> CreateAsync(TCreateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var response = await Client.PostAsJsonAsync(GetRequestUri(model), model);
        return await GetOutputResultAsync(response);
    }

    public async ValueTask<OutputResult> DeleteAsync(TKey id)
    {
        var response = await Client.DeleteAsync(GetRequestUri(id));
        return await GetOutputResultAsync(response);
    }

    public async ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    {
        var response = await Client.GetAsync(GetRequestUri(id));
        return await GetOutputResultAsync<TGetOutput?>(response);
    }

    public async Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(int page, int size, TGetListInput model)
    {
        var uri = new Uri(BaseAddress, $"{model.RelativePath}/{page}?size={size}");
        var response = await Client.PostAsJsonAsync(uri, model);
        return await GetOutputResultAsync<PagedOutputDto<TGetListOutput>>(response);
    }

    public virtual async ValueTask<OutputResult> UpdateAsync(TKey id, TUpdateInput model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (!Validator.TryValidate(model, out var errors))
        {
            return OutputResult.Failed(errors);
        }

        var response = await Client.PutAsJsonAsync(GetRequestUri(model), model);
        return await GetOutputResultAsync(response);
    }

    protected virtual Uri GetRequestUri(HttpInputDtoBase model)
        => new(BaseAddress, $"{RootPath}/{model.RelativePath}");

    protected virtual Uri GetRequestUri(TKey id)
        => new(BaseAddress, $"{RootPath}/{id}");

    protected async Task<OutputResult> GetOutputResultAsync(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                return OutputResult.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonSerializer.Deserialize<OutputResult>(content);
            if (result is null)
            {
                return OutputResult.Failed(Logger, $"Deserialize {nameof(OutputResult)} from content failed");
            }
            return result;

        }
        catch (Exception ex)
        {
            return OutputResult.Failed(Logger, ex);
        }
    }
    protected async Task<OutputResult<TResult>> GetOutputResultAsync<TResult>(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                return OutputResult<TResult>.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonSerializer.Deserialize<OutputResult<TResult>>(content);
            if (result is null)
            {
                return OutputResult<TResult>.Failed(Logger, $"Deserialize {nameof(OutputResult)} from content failed");
            }
            return result;

        }
        catch (Exception ex)
        {
            return OutputResult<TResult>.Failed(Logger, ex);
        }
    }
}
