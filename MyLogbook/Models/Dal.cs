using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class BestWriter
    {
        public string Writer { get; set; }
        public int Count { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Average { get; set; }
    }
    public interface IDal : IDisposable
    {
        List<Book> GetAllBooks();
        void CreateNewBook(string title, string writer, DateTime date, int rating);
    }
    public class Dal : IDal
    {
        private ApplicationDbContext context;
        public Dal()
        {
            ApplicationDbContext context = new ApplicationDbContext();
        }
        public List<Book> GetAllBooks()
        {
            return context.Books.ToList();
        }        
        public void CreateNewBook(string title, string writer, DateTime date, int rating)
        {
            context.Books.Add(new Book { Title = title, Writer = writer, Date = date, Rating = rating });
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}