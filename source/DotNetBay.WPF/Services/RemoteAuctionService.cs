using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Documents;
using DotNetBay.Core;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApi;
using DotNetBay.WebApi.Dto;
using Newtonsoft.Json;

namespace DotNetBay.WPF.Services
{
    public class RemoteAuctionService : IAuctionService
    {
     
        public IQueryable<Auction> GetAll()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(new Uri("http://localhost:49202/api/auctions")).Result;
                result.EnsureSuccessStatusCode();
                string response = result.Content.ReadAsStringAsync().Result;

                List<AuctionDto> auctionDtos = JsonConvert.DeserializeObject<List<AuctionDto>>(response);
                return auctionDtos.Select(a => MapFromDto(a)).AsQueryable();
            }
        }

        public Auction GetById(long id)
        {
            return null; // TODO
        }

        public Auction Save(Auction auction)
        {
            return null; // TODO
        }

        public Bid PlaceBid(Auction auction, double amount)
        {
            return null; // TODO
        }

        private Auction MapFromDto(AuctionDto dto)
        {
            return new Auction()
                {
                    Id = dto.Id,
                    Title = dto.Title, 
                    CurrentPrice = dto.CurrentPrice,
                    CloseDateTimeUtc = dto.CloseDateTimeUtc,
                    StartDateTimeUtc = dto.StartDateTimeUtc,
                    StartPrice = dto.StartPrice,
                    IsClosed = dto.IsClosed,
                    IsRunning = dto.IsRunning,
                    Description = dto.Description,
                    EndDateTimeUtc = dto.EndDateTimeUtc,
                    ActiveBid = new Bid() { Bidder = new Member() { DisplayName = dto.CurrentWinnerName } },
                    Winner = new Member() { DisplayName = dto.CurrentWinnerName },
                    Seller = new Member() { DisplayName = dto.SellerName },
                    Image = this.GetAuctionImage(dto.Id),
                    // Bids = this.MapFromDto(dto.Bids),
                };
        }

        private byte[] GetAuctionImage(long imageId)
        {
            return null;
        }
    }
}
