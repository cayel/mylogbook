using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }
        [Display(Name = "Saison")]
        public int Season { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(0, 10)]
        [Display(Name = "Note")]
        public int Rating { get; set; }
        public string UserId { get; set; }
    }
}