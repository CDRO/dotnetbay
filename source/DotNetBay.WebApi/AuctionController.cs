using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;

namespace DotNetBay.WebApi 
{
    public class AuctionController : ApiController
    {

        private IMainRepository mainRepository;

        public AuctionController()
        {
            this.mainRepository = new EFMainRepository(); ;
        }

        private IAuctionService AuctionService { get; set; }


        [HttpPost]
        [Route("api/auctions")]
        public IHttpActionResult AddAuction(CreateAuctionDto dto)
        {
            if (dto == null)
            {
                return this.StatusCode(HttpStatusCode.Forbidden);
            }
            
            IAuctionService service = new AuctionService(this.mainRepository,
                new SimpleMemberService(this.mainRepository));

            IMemberService memberService = new SimpleMemberService(this.mainRepository);
            
            Auction auction = new Auction();
            auction.Title = dto.Title;
            auction.Description = dto.Description;
            auction.StartPrice = dto.StartPrice;
            auction.StartDateTimeUtc = DateTime.Parse(dto.StartDateTimeUtc);
            auction.EndDateTimeUtc = DateTime.Parse(dto.EndDateTimeUtc);
            auction.Seller = memberService.GetCurrentMember();

            service.Save(auction);
            return this.Ok();
        }

        [HttpGet]
        [Route("api/auctions")]
        public IHttpActionResult GetAuctions()
        {
            IAuctionService service = new AuctionService(this.mainRepository,
                new SimpleMemberService(this.mainRepository));
            var result = service.GetAll();
            return this.Ok(result);
        }
 

    }
}
