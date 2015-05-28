using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("api/auctions/{id}/image")]
        public async Task<IHttpActionResult> AddImage([FromUri] long id)
        {
            Auction auction = AuctionService.GetById(id);
            if (auction == null)
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }

            var streamProvider = await this.Request. Content.ReadAsMultipartAsync();
            foreach (var file in streamProvider.Contents)
            {
                var image = await file.ReadAsByteArrayAsync();
                auction.Image = image;
            }
            AuctionService.Save(auction);
            return this.StatusCode(HttpStatusCode.Created);
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


        [HttpGet]
        [Route("api/auctions/{id}")]
        public IHttpActionResult GetAuctionById([FromUri] long id)
        {

            Auction auction = AuctionService.GetById(id);
            if (auction == null)
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }
            GetAuctionDto auctionDto = new GetAuctionDto();
            auctionDto.CurrentPrice = auction.CurrentPrice;
            auctionDto.SellerName = auction.Seller != null ? auction.Seller.DisplayName : null;
            auctionDto.CurrentWinnerName = auction.ActiveBid != null && auction.ActiveBid.Bidder != null
                ? auction.ActiveBid.Bidder.DisplayName
                : null;
            auctionDto.WinnerName = auction.Winner != null ? auction.Winner.DisplayName : null;

            return this.Ok(auctionDto);
        }

        [HttpGet]
        [Route("api/auctions/{id}/image")]
        public IHttpActionResult GetAuctionImageById([FromUri] long id)
        {

            Auction auction = AuctionService.GetById(id);
            if (auction == null)
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }

            return this.Ok(auction.Image);
        }

    }
}
