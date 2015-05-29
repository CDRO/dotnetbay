using System;
using System.Collections.Generic;
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
        public IHttpActionResult AddAuction(AuctionDto dto)
        {
            if (dto == null)
            {
                return this.StatusCode(HttpStatusCode.Forbidden);
            }
            
            Auction auction = new Auction();
            auction.Title = dto.Title;
            auction.Description = dto.Description;
            auction.StartPrice = dto.StartPrice;
            auction.StartDateTimeUtc = dto.StartDateTimeUtc;
            auction.EndDateTimeUtc = dto.EndDateTimeUtc;
            auction.Seller = MemberService.GetCurrentMember();

            auction = AuctionService.Save(auction);
            dto = MapFromEntity(auction);
            return this.Created(string.Format("api/auctions/{0}", auction.Id), this.MapFromEntity(auction));
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
            return this.Created(string.Format("api/auctions/{0}/image", id),string.Empty);           
        }

        [HttpGet]
        [Route("api/auctions")]
        public IHttpActionResult GetAuctions()
        {
            List<AuctionDto> result = new List<AuctionDto>();
            foreach (Auction a in AuctionService.GetAll())
            {
                result.Add(MapFromEntity(a));
            }
            return this.Ok(result);
        }

        private AuctionDto MapFromEntity(Auction a)
        {
            return new AuctionDto() {
                     Id = a.Id,
                     StartPrice =  a.StartPrice,
                     CurrentPrice = a.CurrentPrice,
                     IsClosed = a.IsClosed,
                     IsRunning =  a.IsRunning,
                     SellerName = a.Seller != null ? a.Seller.DisplayName : null,
                     WinnerName = a.Winner != null ? a.Winner.DisplayName : null,
                     CurrentWinnerName = a.ActiveBid != null && a.ActiveBid.Bidder != null ? a.ActiveBid.Bidder.DisplayName : null,
                     Title =  a.Title,
                     Description =  a.Description,
                     StartDateTimeUtc = a.StartDateTimeUtc,
                     EndDateTimeUtc = a.EndDateTimeUtc,
                     CloseDateTimeUtc =  a.CloseDateTimeUtc
             };
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
            AuctionDto auctionDto = this.MapFromEntity(auction);
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
