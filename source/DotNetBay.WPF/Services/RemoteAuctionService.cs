using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
            using (var client = new HttpClient())
            {
                var result = client.GetAsync(new Uri("http://localhost:49202/api/auctions/" + id)).Result;
                result.EnsureSuccessStatusCode();
                string json = result.Content.ReadAsStringAsync().Result;
                return this.MapFromDto(JsonConvert.DeserializeObject<AuctionDto>(json));
            } 
         }

        public Auction Save(Auction auction)
        {
            using (var client = new HttpClient()) {
                StringContent content = new StringContent(JsonConvert.SerializeObject(auction));
                var result = client.PostAsync(new Uri("http://localhost:49202/api/auctions/"),content).Result;
                result.EnsureSuccessStatusCode();
                string json = result.Content.ReadAsStringAsync().Result;
                return this.MapFromDto(JsonConvert.DeserializeObject<AuctionDto>(json));
            }
        }

        public Bid PlaceBid(Auction auction, double amount)
        {

            using (var client = new HttpClient())
            {
                BidDto bidDto = new BidDto();
                bidDto.Amount = amount;
                StringContent content = new StringContent(JsonConvert.SerializeObject(bidDto));
                var result = client.PostAsync(new Uri("http://localhost:49202/api/auctions/"+auction.Id+"/bids"),content).Result;
                result.EnsureSuccessStatusCode();
                string json = result.Content.ReadAsStringAsync().Result;
                bidDto = JsonConvert.DeserializeObject<BidDto>(json);
                return MapFrom(bidDto);
            }
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
            };
        }

        private Bid MapFrom(BidDto bidDto)
        {
            return new Bid()
            {
                Id = bidDto.Id,
                Amount = bidDto.Amount,
                Accepted = bidDto.Accepted,
                ReceivedOnUtc = bidDto.ReceivedOnUtc,
                Bidder = new Member() { DisplayName = bidDto.BidderName }
            };
        }

        private byte[] GetAuctionImage(long auctionId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(new Uri("http://localhost:49202/api/auctions/" + auctionId)).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsByteArrayAsync().Result;
            }
        }
    }
}
