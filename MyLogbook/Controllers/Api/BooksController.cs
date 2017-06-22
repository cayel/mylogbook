﻿using MyLogbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyLogbook.Controllers.Api
{
    public class BooksController : ApiController
    {
        // GET: api/Books
        public IEnumerable<Book> Get()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            IEnumerable<Book> books = new List<Book>();
            books = context.Books;
            return books.ToList();
        }

        // GET: api/Books/5
        /*
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Books
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Books/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Books/5
        public void Delete(int id)
        {
        }
         */
 
    }
}
