using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DotNetBay.WebApi
{
    public class StatusController : ApiController
    {
        [HttpGet]
        [Route("api/status")]
        public IHttpActionResult AreYouFine()
        {
            return this.Ok("I am fine");
        }

    }
}
