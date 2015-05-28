using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WebApi.Dto
{
    public class GetAuctionDto
    {
        public Double CurrentPrice { get; set; }
        public string SellerName { get; set; }
        public string WinnerName { get; set; }
        public string CurrentWinnerName { get; set; }
    }
}
