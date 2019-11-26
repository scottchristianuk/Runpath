using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Runpath.Media.Net
{
    public class JsonApiClient : IApiClient
    {
        #region Members

        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor.

        public JsonApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #endregion

        /// <summary>
        ///     Calls the <paramref name="path" /> on the configured API and returns the result as type <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">The type to which the call will be deserialised.</typeparam>
        /// <param name="path">The path in the API to call.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string path)
        {
            var httpResult = await _httpClient.GetStringAsync(path)
                                              .ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<T>(httpResult);

            return result;
        }
    }
}