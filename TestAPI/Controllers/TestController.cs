using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zincsearch.Sdk.Client;
using Zincsearch.Sdk.RequestModel;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IZincsearchClient zincsearchClient;


        public TestController(IZincsearchClient zincsearchClient)
        {
            this.zincsearchClient = zincsearchClient;
        }
        [HttpPost]
        public async Task TEST()
        {
            await zincsearchClient.IndexAsync(new IndexRequestModel<SysLog>(new SysLog()));
        }
    }

    public class SysLog
    {


        public int Id { get; set; } = 1;

        public string Name { get; set; } = "1111";


        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
