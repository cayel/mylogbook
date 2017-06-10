using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLogbook.Models;
using System.Collections.Generic;

namespace MyLogbook.Tests
{
    [TestClass]
    public class DalTests
    {
        [TestMethod]
        public void CreateNewBook_GetAllBooks()
        {
            using (IDal dal = new Dal())
            {
                dal.CreateNewBook("Le Titre de mon livre", "Jean Claude", Convert.ToDateTime("12/01/2017"), 8);
                List<Book> books = dal.GetAllBooks();
                Assert.IsNotNull(books);
                Assert.AreEqual(1, books.Count);
                Assert.AreEqual("Le Titre de mon livre", books[0].Title);  
                Assert.AreEqual(8, books[0].Rating);
            }
        }
    }
}
