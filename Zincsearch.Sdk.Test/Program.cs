using Newtonsoft.Json;
using System.Text;
using Zincsearch.Sdk.HttpHelper;

var useraccont = "admin:q123qq..";

var s1 = Convert.ToBase64String(Encoding.UTF8.GetBytes(useraccont));
var data = new HttpRequestModel()
{
    Url = "http://192.168.1.8:4080/api/4444/_doc",
    HttpMethod = HttpMethod.Post,
    HttpContent = new StringContent(JsonConvert.SerializeObject(new SysLog()), Encoding.UTF8, "application/json"),
    RequestContentType = "application/json",
    Heads = new Dictionary<string, string>()
    {
        {"Authorization",$"Basic {s1}" }
    }

};
var res = await HttpRequest.RequestAsync<string>(data);

Console.WriteLine("1111");
Console.WriteLine($"HHH--------{s1}");


public class SysLog
{


    public int Id { get; set; } = 1;

    public string Name { get; set; } = "1111";


    public DateTime DateTime { get; set; } = DateTime.Now;
}