using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WebApi
{
    public class CreateAuctionDto
    {
        
        public string Title { get; set; }
        public double StartPrice { get; set; }
        public string Description { get; set; }
        public string StartDateTimeUtc { get; set; }
        public string EndDateTimeUtc { get; set; }
    }
}