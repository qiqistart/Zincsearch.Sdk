using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Zincsearch.Sdk.HttpHelper;

public static class HttpRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="httpRequestInfo"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<HttpResponseMessage> RequestAsync<T>(HttpRequestModel httpRequestInfo)
    {
        HttpResponseMessage response = await RequestAsync(httpRequestInfo);
        if (response == null)
        {
            throw new Exception("Failed to get the result of the HTTP request");
        }
        return response;
    }

    public static async Task<HttpResponseMessage> RequestAsync(HttpRequestModel httpRequestInfo)
    {
        HttpClientHandler httpclientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (HttpRequestMessage _, X509Certificate2? _, X509Chain? _, SslPolicyErrors _) => true,
            AutomaticDecompression = httpRequestInfo.AutomaticDecompression
        };
        HttpClient client = HttpClientFactory.Create(httpclientHandler);
        client.Timeout = TimeSpan.FromSeconds(httpRequestInfo.Timeout);
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpRequestInfo.HttpMethod, (httpRequestInfo.UriBuilder == null) ? httpRequestInfo.Url : httpRequestInfo.UriBuilder.ToString());
        if (httpRequestInfo.Heads.Any())
        {
            foreach (string key in httpRequestInfo.Heads.Keys)
            {
                httpRequestMessage.Headers.Add(key, httpRequestInfo.Heads[key]);
            }
        }
        httpRequestMessage.Content = httpRequestInfo.HttpContent;
        if (!string.IsNullOrEmpty(httpRequestInfo.RequestContentType))
        {
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(httpRequestInfo.RequestContentType)
            {
                CharSet = httpRequestInfo.Encoding.WebName
            };
        }
        if (!string.IsNullOrEmpty(httpRequestInfo.ResponseContentType))
        {
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(httpRequestInfo.ResponseContentType));
        }
        return await client.SendAsync(httpRequestMessage);
    }


}
