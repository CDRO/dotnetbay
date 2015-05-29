using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Model;
using DotNetBay.WPF.Command;
using DotNetBay.WPF.Services;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    public sealed class AuctionListViewModel
    {

        public AuctionListViewModel()
        {
            this.InitAuctions();
            this.NewAuction = new AuctionCmd<object>();
            this.Bid = new AuctionCmd<object>();
            this.Bid.ExecuteDelegate = ShowBid;
            this.NewAuction.ExecuteDelegate = this.ShowNewAuction;
            this.Bid.CanExecuteDelegate = (o) => true;
            this.NewAuction.CanExecuteDelegate = (o) => true;
        }

        public ObservableCollection<Auction> Auctions { get; private set; }

        public AuctionCmd<object> NewAuction { get; private set; }
        public AuctionCmd<object> Bid { get; private set; }


        public void ShowBid(object parameter)
        {
            var view = new BidView();
            view.ShowDialog();
        }

        public void ShowNewAuction(object parameter)
        {
            SellView view = new SellView();
            view.ShowDialog();
        }

        private void InitAuctions()
        {
            this.Auctions = new ObservableCollection<Auction>();
            App app = (App) App.Current;
            var service = new RemoteAuctionService();

            foreach (var auction in service.GetAll())
            {
                this.Auctions.Add(auction);
            }
        }
    }
}
