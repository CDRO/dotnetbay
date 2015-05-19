using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.WebApi;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Type ctrlType = typeof (StatusController);
            WebApp.Start<Startup>(url: "http://localhost:9001/");
            Console.ReadKey();
        }
    }
}
