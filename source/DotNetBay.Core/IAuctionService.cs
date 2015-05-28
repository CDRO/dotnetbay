using System.Linq;

using DotNetBay.Model;

namespace DotNetBay.Core
{
    public interface IAuctionService
    {
        IQueryable<Auction> GetAll();

        Auction GetById(long id);

        Auction Save(Auction auction);

        Bid PlaceBid(Auction auction, double amount);
    }
}
