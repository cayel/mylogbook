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
                List<BestWriter> bestWriters = dal.GetBestWriters("");
                Assert.IsNull(bestWriters);
            }
        }
    }
}
