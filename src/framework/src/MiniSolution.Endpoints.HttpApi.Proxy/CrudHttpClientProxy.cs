using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Logging;

using MiniSolution.Business.Contracts;
using MiniSolution.Business.Contracts.DTO;

using Newtonsoft.Json;

namespace MiniSolution.Endpoints.HttpApi.Proxy;

public abstract class CrudHttpClientProxy<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput> : HttpApiClientProxy, ICrudApplicationService<TKey, TGetOutput, TGetListOutput, TGetListInput, TCreateInput, TUpdateInput>
    where TGetListInput : class
    where TGetListOutput : class
    where TGetOutput : class
    where TCreateInput : class
    where TUpdateInput : class
{
    protected CrudHttpClientProxy(IServiceProvider services) : base(services)
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

        var response = await Client.PostAsJsonAsync(GetRequestUri(), model);
        return await GetOutputResultAsync(response);
    }

    public virtual async ValueTask<OutputResult> DeleteAsync(TKey id)
    {
        var response = await Client.DeleteAsync(GetRequestUri(id));
        return await GetOutputResultAsync(response);
    }

    public virtual async ValueTask<OutputResult<TGetOutput?>> GetAsync(TKey id)
    {
        var response = await Client.GetAsync(GetRequestUri(id));
        return await GetOutputResultAsync<TGetOutput?>(response);
    }

    public virtual async Task<OutputResult<PagedOutputDto<TGetListOutput>>> GetListAsync(int page, int size, TGetListInput model)
    {
        var uri = GetRequestUri($"?page={page}&size={size}", model);
        var response = await Client.GetAsync(uri);
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

        var response = await Client.PutAsJsonAsync(GetRequestUri(), model);
        return await GetOutputResultAsync(response);
    }

    protected virtual Uri GetRequestUri(string otherPath, object args)
    {
        string? queryString = default;
        if (args is not null)
        {
            queryString = args.GetType().GetProperties().Where(p => p.CanRead).Select(m => $"{m.Name}={m.GetValue(args)}").Aggregate((prev, next) => $"{prev}&{next}");
        }

        var hasQueryMark = otherPath.IndexOf('?') > -1;


        return new(BaseAddress, $"{RootPath}/{otherPath}{(hasQueryMark ? queryString : $"?{queryString}")}");
    }

    protected virtual Uri GetRequestUri(string otherPath = default)
        => new(BaseAddress, $"{RootPath}/{otherPath}");

    protected virtual Uri GetRequestUri(TKey id)
        => new(BaseAddress, $"{RootPath}/{id}");

    protected async Task<OutputResult> GetOutputResultAsync(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        LogRequestUri(response);

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                return OutputResult.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonConvert.DeserializeObject<OutputResult>(content);
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

    private void LogRequestUri(HttpResponseMessage response)
    {
        Logger.LogError("Request uri is {0}", response?.RequestMessage?.RequestUri?.AbsolutePath);
    }

    protected async Task<OutputResult<TResult>> GetOutputResultAsync<TResult>(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        LogRequestUri(response);

        try
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content.IsNullOrEmpty())
            {
                return OutputResult<TResult>.Failed(Logger, "Content from HttpContent is null or empty");
            }
            var result = JsonConvert.DeserializeObject<OutputResult<TResult>>(content);
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
