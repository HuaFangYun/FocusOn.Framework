using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Boloni.DataTransfers;

using Microsoft.Extensions.Logging;

namespace Boloni.Client.Http.Abstractions
{
    public static class ClientProxyExtensions
    {
        public static async Task<OutputResult?> ToResultAsync(this HttpResponseMessage response,ILogger logger)
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
