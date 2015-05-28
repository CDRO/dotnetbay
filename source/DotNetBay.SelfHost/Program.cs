using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.WebApi;
using DotNetBay.WebApi.Controller;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Type statusCtrlType = typeof (StatusController);
            Type auctionCtrlType = typeof (AuctionController);
            Type bidCtrlType = typeof (BidController);

            WebApp.Start<Startup>(url: "http://localhost:9001/");
            Console.ReadKey();
        }
    }
}
