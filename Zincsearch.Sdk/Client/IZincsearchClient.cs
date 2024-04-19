using Zincsearch.Sdk.Model;
using Zincsearch.Sdk.RequestModel;
using Zincsearch.Sdk.ResultModel;

namespace Zincsearch.Sdk.Client;

public interface IZincsearchClient
{

    Task<SdkResult<IndexReponse>> IndexAsync<T>(IndexRequestModel<T> indexRequestModel);
}

