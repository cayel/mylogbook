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
    public class ComicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comics
        public ActionResult Index(string searchBd, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchBd != null)
            {
                page = 1;
            }
            else
            {
                searchBd = currentFilter;
            }
            ViewBag.CurrentFilter = searchBd;

            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<Comic> comics = new List<Comic>();

            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewBag.SerieSortParm = sortOrder == "Serie" ? "serie_desc" : "Serie";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.VolumeSortParm = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewBag.ScenaristSortParm = sortOrder == "Scenarist" ? "scenarist_desc" : "Scenarist";
            ViewBag.CartoonistSortParm = sortOrder == "Cartoonist" ? "cartoonist_desc" : "Cartoonist";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_asc" : "Rating";

            if (!string.IsNullOrEmpty(userid))
            {
                comics = context.Comics.Where(x => x.UserId == userid);
                if (!String.IsNullOrEmpty(searchBd))
                {
                    string searchBdLower = searchBd.ToLower();
                    comics = comics.Where(s => s.Serie.ToLower().Contains(searchBdLower) || s.Title.ToLower().Contains(searchBdLower) || (!string.IsNullOrEmpty(s.Scenarist) && s.Scenarist.ToLower().Contains(searchBdLower)) 
                            || (!string.IsNullOrEmpty(s.Cartoonist) && s.Cartoonist.ToLower().Contains(searchBdLower)));
                }
                switch (sortOrder)
                {
                    case "serie_desc":
                        comics = comics.OrderByDescending(s => s.Serie);
                        break;
                    case "Serie":
                        comics = comics.OrderBy(s => s.Serie);
                        break;
                    case "title_desc":
                        comics = comics.OrderByDescending(s => s.Title);
                        break;
                    case "Title":
                        comics = comics.OrderBy(s => s.Title);
                        break;
                    case "volume_desc":
                        comics = comics.OrderByDescending(s => s.Volume);
                        break;
                    case "Volume":
                        comics = comics.OrderBy(s => s.Volume);
                        break;
                    case "scenarist_desc":
                        comics = comics.OrderByDescending(s => s.Scenarist);
                        break;
                    case "Scenarist":
                        comics = comics.OrderBy(s => s.Scenarist);
                        break;
                    case "cartoonist_desc":
                        comics = comics.OrderByDescending(s => s.Cartoonist);
                        break;
                    case "Cartoonist":
                        comics = comics.OrderBy(s => s.Cartoonist);
                        break;
                    case "date_asc":
                        comics = comics.OrderBy(s => s.Date);
                        break;
                    case "rating_asc":
                        comics = comics.OrderBy(s => s.Rating);
                        break;
                    case "Rating":
                        comics = comics.OrderByDescending(s => s.Rating);
                        break;

                    default:
                        comics = comics.OrderByDescending(s => s.Date);
                        break;
                }

                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(comics.ToPagedList(pageNumber, pageSize));
            }
            else return View("Error");
        }


        // GET: Comics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comic comic = db.Comics.Find(id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }

        // GET: Comics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Serie,Title,Volume,Scenarist,Cartoonist,Date,Rating,UserId")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                comic.UserId = userid;
                db.Comics.Add(comic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comic);
        }

        // GET: Comics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comic comic = db.Comics.Find(id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }

        // POST: Comics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Serie,Title,Volume,Scenarist,Cartoonist,Date,Rating,UserId")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comic);
        }

        // GET: Comics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comic comic = db.Comics.Find(id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comic comic = db.Comics.Find(id);
            db.Comics.Remove(comic);
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
