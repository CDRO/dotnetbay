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
        private IAuctionService service;

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
            return View();
        }

        // GET: Auctions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            IMemberService members = new SimpleMemberService(this.repo);
            auction.Seller = members.GetCurrentMember();
            this.service.Save(auction);
            return this.RedirectToAction("Index");
        }

        // GET: Auctions/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Auctions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Auctions/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auctions/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
