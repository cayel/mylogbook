using MyLogbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace MyLogbook.Controllers
{
    public class DashboardController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private dynamic getUserBooksGroupByWriter (string userid, int countWriters)
        {
            var dataWriters = context.Books.Where(x => x.UserId == userid).GroupBy(t => new { Writer = t.Writer })
                .Where(p => p.Count() > 1)
                .Select(g => new
                {
                    Count = g.Count(),
                    Average = g.Average(p => p.Rating),
                    Writer = g.Key.Writer
                }).OrderByDescending(x => x.Average).Take(countWriters).ToList();
            return dataWriters;
        }
        private List<BestWriter> getBestWritersFromList(dynamic listBestWriters)
        {
            List<BestWriter> bestWriters = new List<BestWriter>();
            foreach (var item in listBestWriters)
            {
                bestWriters.Add(new BestWriter { Writer = item.Writer, Count = item.Count, Average = item.Average });
            }
            return bestWriters;
        }
        public List<BestWriter> GetBestWriters(string userId)
        {           
            var dataWriters = getUserBooksGroupByWriter(userId,5);
            List<BestWriter> bestWriters = new List<BestWriter>();
            bestWriters = getBestWritersFromList(dataWriters);            
            return (bestWriters);
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            List<BestWriter> bestWriters = GetBestWriters(userId);
            return View(bestWriters);
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