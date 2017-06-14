using MyLogbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLogbook.ViewModels
{
    public class FavoriteViewModel
    {
        public List<BestWriter> BestWriters { get; set; }
        public List<BestDirector> BestDirectors { get; set; }
    }
}