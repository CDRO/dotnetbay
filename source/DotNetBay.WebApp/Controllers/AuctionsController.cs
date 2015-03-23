using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : ApiController
    {
        // GET: api/Auctions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auctions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auctions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Auctions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auctions/5
        public void Delete(int id)
        {
        }
    }
}
