using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class FavoriteConcertHall
    {
        public string ConcertHall { get; set; }
        public int Count { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Average { get; set; }
    }
}