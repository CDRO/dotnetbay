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
        [Route("api/auctions/{auctionId}/bids")]
        public IHttpActionResult CreateBid([FromUri]long auctionId, [FromBody] BidDto dto)
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
            dto.BidderName = MemberService.GetCurrentMember().DisplayName;
            dto.ReceivedOnUtc = DateTime.UtcNow;
            Bid bid = MapFrom(dto);
            bid = Repository.Add(bid);
            return this.Created(string.Format("api/auctions/{0}/bids/{1}", auctionId,bid.Id), MapFrom(dto));
        }

        private Bid MapFrom(BidDto bid)
        {
            return new Bid()
            {
                Id = bid.Id,
                ReceivedOnUtc = bid.ReceivedOnUtc,
                Amount = bid.Amount,
                Accepted = bid.Accepted,
                Bidder = new Member() {DisplayName = bid.BidderName},
                TransactionId = bid.TransactionId
            };
        }

        private BidDto MapFrom(Bid bid)
        {
            return new BidDto()
            {
                Id = bid.Id,
                ReceivedOnUtc = bid.ReceivedOnUtc,
                Amount = bid.Amount,
                Accepted = bid.Accepted,
                BidderName = bid.Bidder != null ? bid.Bidder.DisplayName: null,
                TransactionId = bid.TransactionId
            };
        }
    }
}
