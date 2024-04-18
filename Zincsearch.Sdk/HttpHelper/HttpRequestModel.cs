using System.Net;
using System.Text;

namespace Zincsearch.Sdk.HttpHelper;

public class HttpRequestModel
{
    public string Url { get; set; }

    public UriBuilder? UriBuilder { get; set; }

    public HttpMethod HttpMethod { get; set; } = System.Net.Http.HttpMethod.Post;


    public Dictionary<string, string> Heads { get; set; } = new Dictionary<string, string>();


    public string RequestContentType { get; set; } = "application/json";


    public string ResponseContentType { get; set; } = "application/json";


    public HttpContent HttpContent { get; set; } = new ByteArrayContent(Array.Empty<byte>());


    public Encoding Encoding { get; set; } = System.Text.Encoding.Default;


    public long Timeout { get; set; } = 180L;


    public DecompressionMethods AutomaticDecompression { get; set; } = DecompressionMethods.None;

}

