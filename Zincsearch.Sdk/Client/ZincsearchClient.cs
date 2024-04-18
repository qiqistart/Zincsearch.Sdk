using Microsoft.Extensions.Options;
using Zincsearch.Sdk.Config;
using Zincsearch.Sdk.Model;

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
    public async Task<IndexReponse> IndexAsync<T>(string Index,T DocumentContent)
    {

    }
}

