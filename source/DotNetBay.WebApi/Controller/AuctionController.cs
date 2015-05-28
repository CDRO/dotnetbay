using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApi.Dto;
using Microsoft.SqlServer.Server;

namespace DotNetBay.WebApi 
{
    public class AuctionController : ApiController
    {

        private IMainRepository mainRepository;

        public AuctionController()
        {
            this.mainRepository = new EFMainRepository();
            AuctionService = new AuctionService(this.mainRepository,
                new SimpleMemberService(this.mainRepository));

            MemberService = new SimpleMemberService(this.mainRepository);
            
        }

        private IAuctionService AuctionService { get; set; }


        private IMemberService MemberService { get; set; }

        [HttpPost]
        [Route("api/auctions")]
        public IHttpActionResult AddAuction(CreateAuctionDto dto)
        {
            if (dto == null)
            {
                return this.StatusCode(HttpStatusCode.Forbidden);
            }
            
            Auction auction = new Auction();
            auction.Title = dto.Title;
            auction.Description = dto.Description;
            auction.StartPrice = dto.StartPrice;
            auction.StartDateTimeUtc = DateTime.Parse(dto.StartDateTimeUtc);
            auction.EndDateTimeUtc = DateTime.Parse(dto.EndDateTimeUtc);
            auction.Seller = MemberService.GetCurrentMember();

            AuctionService.Save(auction);
            return this.Ok();
        }

        [HttpGet]
        [Route("api/auctions")]
        public IHttpActionResult GetAuctions()
        {
            
            var result = AuctionService.GetAll().Select(a =>
                 new GetAuctionDto() {
                     CurrentPrice = a.CurrentPrice,
                     SellerName = a.Seller.DisplayName,
                     CurrentWinnerName = a.ActiveBid.Bidder.DisplayName,
                     WinnerName = a.Winner.DisplayName
             });

            return this.Ok(result);
        }
 

    }
}
