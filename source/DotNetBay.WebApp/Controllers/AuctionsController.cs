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
    public class AuctionsController : Controller
    {
        private IMainRepository repo;
        private AuctionService service;

        public AuctionsController()
        {
            this.repo = new EFMainRepository();
            this.service = new AuctionService(
                this.repo,
                new SimpleMemberService(this.repo)
            );
        }

        // GET: Auctions
        public ActionResult Index()
        {
            return View(this.service.GetAll());
        }

        // GET: Auctions/Details/5
        public ActionResult Details(int id)
        {
            return View(this.service.GetById(id));
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(NewAuctionViewModel auctionViewModel)
        {
            if (ModelState.IsValid)
            {
                var members = new SimpleMemberService(this.repo);

                Auction auction = new Auction();
                auction.Title = auctionViewModel.Titel;
                auction.StartPrice = auctionViewModel.StartPrice;
                auction.Description = auctionViewModel.Description;
                auction.StartDateTimeUtc = auctionViewModel.StartDateTimeUtc;
                auction.EndDateTimeUtc = auctionViewModel.EndDateTimeUtc;
                auction.Image = auctionViewModel.Image;

                auction.Seller = members.GetCurrentMember();

                this.service.Save(auction);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
