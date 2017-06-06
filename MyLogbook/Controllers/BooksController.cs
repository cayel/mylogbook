﻿using System;
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
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index(string searchBook, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchBook != null)
            {
                page = 1;
            }
            else
            {
                searchBook = currentFilter;
            }
            ViewBag.CurrentFilter = searchBook;

            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<Book> books = new List<Book>();

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "";
            ViewBag.WriterSortParm = String.IsNullOrEmpty(sortOrder) ? "writer_asc" : "";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.RatingSortParm = String.IsNullOrEmpty(sortOrder) ? "rating_desc" : "";

            if (!string.IsNullOrEmpty(userid))
            {
                books = context.Books.Where(x => x.UserId == userid);
                if (!String.IsNullOrEmpty(searchBook))
                {
                    books = books.Where(s => s.Title.ToLower().Contains(searchBook.ToLower()) || s.Writer.ToLower().Contains(searchBook.ToLower()));
                }
                switch (sortOrder)
                {
                    case "title_asc":
                        books = books.OrderBy(s => s.Title);
                        break;
                    case "writer_asc":
                        books = books.OrderBy(s => s.Writer);
                        break;
                    case "date_desc":
                        books = books.OrderByDescending(s => s.Date);
                        break;
                    case "rating_desc":
                        books = books.OrderByDescending(s => s.Rating);
                        break;
                    default:
                        books = books.OrderByDescending(s => s.Date);
                        break;
                }
                
                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(books.ToPagedList(pageNumber, pageSize));
            }
            else return View("Error");
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Writer,Date,Rating,UserId")] Book book)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                book.UserId = userid;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Writer,Date,Rating,UserId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
