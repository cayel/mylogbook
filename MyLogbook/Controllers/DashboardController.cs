using MyLogbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using MyLogbook.ViewModels;
using Newtonsoft.Json;


namespace MyLogbook.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            IDal dal = new Dal();
            FavoriteViewModel vm = new FavoriteViewModel 
            { 
                BestWriters = dal.GetBestWriters(userId),
                BestDirectors = dal.GetBestDirectors(userId),
                BestConcertHall = dal.GetFavoriteConcertHalls(userId)
            };

            HistoMedia histoBooks = new HistoMedia();
            histoBooks = dal.GetUserHistoBooksCountPerYer(userId);

            HistoMedia histoMovies = new HistoMedia();
            histoMovies = dal.GetUserHistoMoviesCountPerYer(userId);

            HistoMedia histoConcerts = new HistoMedia();
            histoConcerts = dal.GetUserHistoConcertsCountPerYer(userId);

            List<String> yearListBooks = histoBooks.Year;
            List<int> countListBooks = histoBooks.Count;
            List<String> yearListMovies = histoMovies.Year;
            List<int> countListMovies = histoMovies.Count;
            List<String> yearListConcerts = histoConcerts.Year;
            List<int> countListConcerts = histoConcerts.Count;

            ViewBag.YearListBooks = JsonConvert.SerializeObject(yearListBooks);
            ViewBag.CountListBooks = JsonConvert.SerializeObject(countListBooks);

            ViewBag.YearListMovies = JsonConvert.SerializeObject(yearListMovies);
            ViewBag.CountListMovies = JsonConvert.SerializeObject(countListMovies);

            ViewBag.YearListConcerts = JsonConvert.SerializeObject(yearListConcerts);
            ViewBag.CountListConcerts = JsonConvert.SerializeObject(countListConcerts);

            return View(vm);
        }
        public ActionResult DrawGraphBook()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<Book> books = new List<Book>();
            
            if (!string.IsNullOrEmpty(userid))
            {
                books = context.Books.Where(x => x.UserId == userid);

                var unique_years = books.Select(s => s.Date.Year).Distinct().ToList();

                string[] _yval = new string[unique_years.Count];    
                int count = 0;
                foreach (var item in unique_years)
                {
                    int booksCount = context.Books.Where(x => x.UserId == userid && (x.Date.Year == item)).Count();                 
                    _yval[count++] = booksCount.ToString();    
                }
                //here the chart is going on

                var bytes = new Chart(width: 600, height: 300, theme: ChartTheme.Yellow)
                 .AddTitle("Lectures")
                .AddSeries(
                chartType: "Column", 
                 xValue: unique_years,
                 yValues: _yval)
                .GetBytes("png");
                
                return File(bytes, "image/png");
            }
            else return View("Error");
        }
        public ActionResult DrawGraphMovie()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<Movie> movies = new List<Movie>();

            if (!string.IsNullOrEmpty(userid))
            {
                movies = context.Movies.Where(x => x.UserId == userid);

                var unique_years = movies.Select(s => s.Date.Year).Distinct().ToList();

                string[] _yval = new string[unique_years.Count];
                int count = 0;
                foreach (var item in unique_years)
                {
                    int moviesCount = context.Movies.Where(x => x.UserId == userid && (x.Date.Year == item)).Count();
                    _yval[count++] = moviesCount.ToString();
                }
                //here the chart is going on

                var bytes = new Chart(width: 600, height: 300, theme: ChartTheme.Yellow)
                .AddTitle("Films")
               .AddSeries(
               chartType: "Column",
                xValue: unique_years,
                yValues: _yval)
               .GetBytes("png");

                return File(bytes, "image/png");
            }
            else return View("Error");
        }
        public ActionResult DrawGraphConcert()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();

            IEnumerable<Concert> concerts = new List<Concert>();

            if (!string.IsNullOrEmpty(userid))
            {
                concerts = context.Concerts.Where(x => x.UserId == userid);

                var unique_years = concerts.Select(s => s.Date.Year).Distinct().ToList();

                string[] _yval = new string[unique_years.Count];
                int count = 0;
                foreach (var item in unique_years)
                {
                    int concertsCount = context.Concerts.Where(x => x.UserId == userid && (x.Date.Year == item)).Count();
                    _yval[count++] = concertsCount.ToString();
                }
                //here the chart is going on

                var bytes = new Chart(width: 600, height: 300, theme: ChartTheme.Yellow)
                .AddTitle("Concerts")
               .AddSeries(
               chartType: "Column",
                xValue: unique_years,
                yValues: _yval)
               .GetBytes("png");

                return File(bytes, "image/png");
            }
            else return View("Error");
        }
    }
}