using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WebApi
{
    public class AuctionDto
    {
        public long Id { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public bool IsClosed { get; set; }
        public bool IsRunning { get; set; }
        public string SellerName { get; set; }
        public string WinnerName { get; set; }
        public string CurrentWinnerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }
        public DateTime CloseDateTimeUtc { get; set; }
    }
}