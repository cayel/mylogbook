using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}