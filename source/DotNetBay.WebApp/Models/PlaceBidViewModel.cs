using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetBay.Model;

namespace DotNetBay.WebApp.Models
{
    public class PlaceBidViewModel
    {
        public int AuctionId { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public double CurrentBid { get; set; }
    }
}