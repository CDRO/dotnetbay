using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;

namespace DotNetBay.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            // DotNetBay Init
            IMainRepository repo = new EFMainRepository();
            repo.SaveChanges();

            AuctionRunner = new AuctionRunner(repo);
            AuctionRunner.Start();
        }

        public static AuctionRunner AuctionRunner { get; private set; }
    }
}
