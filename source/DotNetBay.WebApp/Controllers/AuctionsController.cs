using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;

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
        public ActionResult Create(Auction auction)
        {
            var members = new SimpleMemberService(this.repo);
            auction.Seller = members.GetCurrentMember();
            this.service.Save(auction);
            return RedirectToAction("Index");
        }

    }
}
