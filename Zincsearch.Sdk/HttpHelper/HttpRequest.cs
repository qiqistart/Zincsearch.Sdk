using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Zincsearch.Sdk.Common;

namespace Zincsearch.Sdk.HttpHelper;

public static class HttpRequest
{
    public static async Task<T?> RequestAsync<T>(HttpRequestModel httpRequestInfo)
    {
        HttpResponseMessage response = await RequestAsync(httpRequestInfo);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Http请求失败-" + JsonConvert.SerializeObject(response));
        }

        try
        {
            Type resType = typeof(T);
            object resData2 = null;
            if (resType == typeof(string))
            {
                resData2 = await response.Content.ReadAsStringAsync();
            }
            else if (resType == typeof(Stream))
            {
                resData2 = await response.Content.ReadAsStreamAsync();
            }
            else if (resType == typeof(byte[]))
            {
                resData2 = await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                string responseContentType = httpRequestInfo.ResponseContentType;
                if (1 == 0)
                {
                }

                object obj = responseContentType switch
                {
                    "application/json" => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()),
                    "text/xml" => (await response.Content.ReadAsStringAsync()).DeserializationXml<T>(),
                    "application/xml" => (await response.Content.ReadAsStringAsync()).DeserializationXml<T>(),
                    _ => resData2,
                };
                if (1 == 0)
                {
                }

                resData2 = obj;
            }

            return resData2.ChangeType<T>();
        }
        catch
        {
            throw new Exception("获取Http请求结果失败");
        }
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
