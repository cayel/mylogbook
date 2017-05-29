using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class Concert
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Artiste")]
        public string Artist { get; set; }
        [Display(Name = "Avec")]
        public string With { get; set; }
        [Required]
        [Display(Name = "Salle")]
        public string Hall { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(0, 10)]
        [Display(Name = "Note")]
        public int Rating { get; set; }
        public string UserId { get; set; }
    }
}