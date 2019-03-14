using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static bool _mustSendError = true;
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _mustSendError = !_mustSendError;
            if (_mustSendError) { throw new Exception("damned!"); }

            return new List<string>
            {
                "value 1",
                "value 2",
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _mustSendError = !_mustSendError;
            if (_mustSendError) { throw new Exception("damned!"); }

            return "value";
        }
    }
}
