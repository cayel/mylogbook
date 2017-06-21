using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class HistoMedia
    {
        public List<string> Year { get; set; }
        public List<int> Count { get; set; }
        public HistoMedia()
        {
            Year = new List<string>();
            Count = new List<int>();
        }
    }
}