using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Unity;

namespace ETHotfix.Module.AspNetCore
{
    /// <summary>
    /// 测试类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value", "hashCode:" + this.GetHashCode() };
        }
    }
}