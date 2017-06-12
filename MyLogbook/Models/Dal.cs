using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public interface IDal : IDisposable
    {
        List<Book> GetAllBooks();
        List<BestWriter> GetBestWriters(string userId);
    }
    public class Dal : IDal
    {
        private ApplicationDbContext context;
        public Dal()
        {
            context = new ApplicationDbContext();
        }
        public List<Book> GetAllBooks()
        {
            return context.Books.ToList();
        }
        private dynamic getUserBooksGroupByWriter(string userid, int countWriters)
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
            var dataWriters = getUserBooksGroupByWriter(userId, 5);
            List<BestWriter> bestWriters = new List<BestWriter>();
            bestWriters = getBestWritersFromList(dataWriters);
            return (bestWriters);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}