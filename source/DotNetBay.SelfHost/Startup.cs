using System;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(DotNetBay.SelfHost.Startup))]

namespace DotNetBay.SelfHost
{
    public class Startup
    {
        public static IAuctionRunner AuctionRunner { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);

            app.UseWebApi(config); 
        }
    }
}
