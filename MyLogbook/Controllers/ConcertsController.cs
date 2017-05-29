using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyLogbook.Models;
using Microsoft.AspNet.Identity;

namespace MyLogbook.Controllers
{
    public class ConcertsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Concerts
        public ActionResult Index(string searchArtist, string searchHall)
        {
            var HallLst = new List<string>();

            var HallQry = from d in db.Concerts
                          orderby d.Hall
                          select d.Hall;

            HallLst.AddRange(HallQry.Distinct());
            ViewBag.SearchHall = new SelectList(HallLst);

            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<Concert> concerts = new List<Concert>();
            if (!string.IsNullOrEmpty(userid))
            {
                concerts = context.Concerts.Where(x => x.UserId == userid);
                if (!String.IsNullOrEmpty(searchArtist))
                {
                    concerts = concerts.Where(s => s.Artist.ToLower().Contains(searchArtist.ToLower()) || s.With.ToLower().Contains(searchArtist.ToLower()));
                }
                if (!String.IsNullOrEmpty(searchHall))
                {
                    concerts = concerts.Where(s => s.Hall.ToLower().Contains(searchHall.ToLower()));
                }
                concerts = concerts.OrderByDescending(s => s.Date);
                return View(concerts);
            }
            else return View("Error");
        }

        // GET: Concerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concert concert = db.Concerts.Find(id);
            if (concert == null)
            {
                return HttpNotFound();
            }
            return View(concert);
        }

        // GET: Concerts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Artist,With,Hall,Date,Rating,UserId")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                concert.UserId = userid;
                db.Concerts.Add(concert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(concert);
        }

        // GET: Concerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concert concert = db.Concerts.Find(id);
            if (concert == null)
            {
                return HttpNotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Artist,With,Hall,Date,Rating,UserId")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concert concert = db.Concerts.Find(id);
            if (concert == null)
            {
                return HttpNotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concert concert = db.Concerts.Find(id);
            db.Concerts.Remove(concert);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
