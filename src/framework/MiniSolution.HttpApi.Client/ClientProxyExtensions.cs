using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using MiniSolution.ApplicationContracts.DTO;

namespace MiniSolution.HttpApi.Client
{
    public static class ClientProxyExtensions
    {
        public static async Task<OutputResult?> ToResultAsync(this HttpResponseMessage response, ILogger logger)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            try
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<OutputResult?>();

            }
            catch (Exception ex)
            {
                return OutputResult.Failed(logger, ex);
            }
        }
    }
}
