using System;
using System.Net.Http;
using System.Net.Mime;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace Runpath.Media.Net
{
    public static class HttpClientStub
    {
        /// <summary>
        ///     A valid URI is required for stubbing the HttpClient
        /// </summary>
        public const string BaseUri = @"http://a.valid-url.com";

        /// <summary>
        ///     Instantiates and configures an HttpClient stub for unit testing
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static HttpClient Create(Action<MockHttpMessageHandler> configure)
        {
            var handler = new MockHttpMessageHandler();
            configure?.Invoke(handler);

            var httpClient = handler.ToHttpClient();
            httpClient.BaseAddress = new Uri(BaseUri);
            return httpClient;
        }

        /// <summary>
        ///     Sets up the <paramref name="path" /> on the mock HttpClient
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MockedRequest ForPath(this MockHttpMessageHandler handler, string path)
        {
            return handler.When($"{BaseUri}/{path}");
        }

        /// <summary>
        ///     Returns the JSON for the specified <paramref name="item" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static MockedRequest ReturnJsonFor<T>(this MockedRequest request, T item)
        {
            request.Respond(MediaTypeNames.Application.Json, JsonConvert.SerializeObject(item));

            return request;
        }
    }
}