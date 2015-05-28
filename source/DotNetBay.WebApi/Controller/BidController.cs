using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApi.Dto;

namespace DotNetBay.WebApi.Controller
{
    public class BidController : ApiController
    {

        public BidController()
        {
            this.Repository = new EFMainRepository();
            this.MemberService = new SimpleMemberService(Repository);
            AuctionService = new AuctionService(this.Repository, new SimpleMemberService(this.Repository));
            
        }

        private IMainRepository Repository { get; set;  }

        private IMemberService MemberService { get; set; }

        private IAuctionService AuctionService { get; set; }

        [HttpPost]
        [Route("api/auctions/{auctionId}/bid")]
        public IHttpActionResult CreateBid([FromUri]long auctionId, [FromBody] CreateBidDto dto)
        {
            if (dto == null)
            {
                return this.StatusCode(HttpStatusCode.Forbidden);
            }

            Auction auction = AuctionService.GetById(auctionId);
            if (auction == null)
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }
            Member bidder = MemberService.GetCurrentMember();
            Repository.Add(new Bid() { ReceivedOnUtc = DateTime.UtcNow, Bidder = bidder, Amount = dto.Amount, Auction = auction });


            return this.StatusCode(HttpStatusCode.Created);

        }
    }
}
