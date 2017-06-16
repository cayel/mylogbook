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
using PagedList;


namespace MyLogbook.Controllers
{
    public class TvShowsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TvShows
        public ActionResult Index(string searchTvShow, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchTvShow != null)
            {
                page = 1;
            }
            else
            {
                searchTvShow = currentFilter;
            }
            ViewBag.CurrentFilter = searchTvShow;

            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<TvShow> tvShows = new List<TvShow>();

            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.SeasonSortParm = sortOrder == "Season" ? "season_desc" : "Season";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_asc" : "Rating";

            if (!string.IsNullOrEmpty(userid))
            {
                tvShows = context.TvShows.Where(x => x.UserId == userid);
                if (!String.IsNullOrEmpty(searchTvShow))
                {
                    tvShows = context.TvShows.Where(s => s.Title.ToLower().Contains(searchTvShow.ToLower()));
                }
                switch (sortOrder)
                {
                    case "title_desc":
                        tvShows = tvShows.OrderByDescending(s => s.Title);
                        break;
                    case "Title":
                        tvShows = tvShows.OrderBy(s => s.Title);
                        break;
                    case "season_desc":
                        tvShows = tvShows.OrderByDescending(s => s.Season);
                        break;
                    case "Season":
                        tvShows = tvShows.OrderBy(s => s.Season);
                        break;
                    case "date_asc":
                        tvShows = tvShows.OrderBy(s => s.Date);
                        break;
                    case "rating_asc":
                        tvShows = tvShows.OrderBy(s => s.Rating);
                        break;
                    case "Rating":
                        tvShows = tvShows.OrderByDescending(s => s.Rating);
                        break;

                    default:
                        tvShows = tvShows.OrderByDescending(s => s.Date);
                        break;
                }

                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(tvShows.ToPagedList(pageNumber, pageSize));
            }
            else return View("Error");
        }


        // GET: TvShows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShow tvShow = db.TvShows.Find(id);
            if (tvShow == null)
            {
                return HttpNotFound();
            }
            return View(tvShow);
        }

        // GET: TvShows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Season,Date,Rating,UserId")] TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                tvShow.UserId = userid;
                db.TvShows.Add(tvShow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tvShow);
        }

        // GET: TvShows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShow tvShow = db.TvShows.Find(id);
            if (tvShow == null)
            {
                return HttpNotFound();
            }
            return View(tvShow);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Season,Date,Rating,UserId")] TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tvShow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tvShow);
        }

        // GET: TvShows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShow tvShow = db.TvShows.Find(id);
            if (tvShow == null)
            {
                return HttpNotFound();
            }
            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TvShow tvShow = db.TvShows.Find(id);
            db.TvShows.Remove(tvShow);
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
