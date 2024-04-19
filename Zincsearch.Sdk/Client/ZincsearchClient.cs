using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Zincsearch.Sdk.Config;
using Zincsearch.Sdk.HttpHelper;
using Zincsearch.Sdk.Model;
using Zincsearch.Sdk.RequestModel;
using Zincsearch.Sdk.ResultModel;
using Zincsearch.Sdk.ZincsearchRequestPath;

namespace Zincsearch.Sdk.Client;

public class ZincsearchClient : IZincsearchClient
{
    /// <summary>
    /// 配置
    /// </summary>
    private readonly ZincsearchConfig zincsearchConfig;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public ZincsearchClient(IOptions<ZincsearchConfig> options)
    {
        zincsearchConfig = options.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Index">索引名</param>
    /// <param name="DocumentContent">文档</param>
    /// <returns></returns>
    public async Task<SdkResult<IndexReponse>> IndexAsync<T>(IndexRequestModel<T> indexRequestModel)
    {
        try
        {
            var indexName = "";
            if (string.IsNullOrWhiteSpace(indexRequestModel.IndexName))
                indexName = zincsearchConfig.DefaultIndex;

            else
                indexName = indexRequestModel.IndexName;

            var authentication = GetAuthentication();
            var url = string.Format($"{zincsearchConfig.ZincsearchUrl}{RequestPath.Index}", indexName);

            var requestModel = new HttpRequestModel()
            {
                Url = url,
                HttpMethod = HttpMethod.Post,
                HttpContent = new StringContent(JsonConvert.SerializeObject(indexRequestModel.Data),
                Encoding.UTF8,
                "application/json"),
                RequestContentType = "application/json",
                Heads = new Dictionary<string, string>()
            {
               {"Authorization",$"Basic {authentication}" }
            }
            };
            var response = await HttpRequest.RequestAsync<string>(requestModel);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return SdkResult.Fail<IndexReponse>("Unauthorized");
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return SdkResult.Fail<IndexReponse>("InternalServerError");
            }
            var _resultData = JsonConvert.DeserializeObject<IndexReponse>(await response.Content.ReadAsStringAsync());
            return SdkResult.Ok(_resultData);
        }
        catch (Exception ex)
        {
            return SdkResult.Fail<IndexReponse>(ex.Message.ToString());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string GetAuthentication()
    {
        if (zincsearchConfig == null)
        {
            throw new Exception("The configuration is missing");
        }
        string authenticationInfo = $"{zincsearchConfig.UserName}:{zincsearchConfig.PassWord}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationInfo));
    }
}

