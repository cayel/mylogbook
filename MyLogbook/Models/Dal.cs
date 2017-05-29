using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class Dal
    {
        public void GetListWriter ()
        {
            IEnumerable<Book> books = new List<Book>();
            var listWriter = books.Select(s => s.Writer).Distinct().ToList();
        }
/*
 * public void GetMostReadWriters()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userid = User.Identity.GetUserId();
            IEnumerable<Book> books = new List<Book>();
            books = context.Books.Where(x => x.UserId == userid);
            var res = from b in books group b by new { b.Writer } into g select new { g.Key.Writer, value = g.Distinct().Count() };

            foreach (var item in res)
            {
                Console.WriteLine(item.Writer + " " + item.value);
            }
        }
 */

    }
}