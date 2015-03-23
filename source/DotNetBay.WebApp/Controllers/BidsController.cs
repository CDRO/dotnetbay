using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class BidsController : Controller
    {
        private IMainRepository repo;
        private AuctionService service;
        private IMemberService memberService;

        public BidsController()
        {
            this.repo = new EFMainRepository();
            this.memberService = new SimpleMemberService(this.repo); 
            this.service = new AuctionService
                (
                this.repo,
                this.memberService
            );
        }

        // GET: Bids
        public ActionResult Index()
        {
            return View();
        }

        // GET: Bids/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bids/Create
        public ActionResult Create(int id)
        {
            Auction auction =  this.service.GetById(id);
            PlaceBidViewModel viewModel = new PlaceBidViewModel();
            viewModel.AuctionId = id;
            viewModel.Title = auction.Title;
            viewModel.Description = auction.Description;
            viewModel.StartPrice = auction.StartPrice;
            viewModel.CurrentPrice = auction.CurrentPrice;
            viewModel.CurrentBid = auction.CurrentPrice + 1;
            return View(viewModel);
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult Create(PlaceBidViewModel viewModel)
        {
            try
            {
                Member bidder = memberService.GetCurrentMember();
                Auction auction = this.service.GetById(viewModel.AuctionId);
                auction.ActiveBid = this.service.PlaceBid(bidder, auction, viewModel.CurrentBid);
                auction.Bids.Add(auction.ActiveBid);
                auction.CurrentPrice = auction.ActiveBid.Amount;
                this.service.Save(auction);
                return RedirectToAction("Index","Auctions");
            }
            catch
            {
                return View(viewModel);
            }
        }
    }
}
