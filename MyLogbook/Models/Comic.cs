using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLogbook.Models
{
    public class Comic
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }
        [Display(Name = "Scénariste")]
        public string Scenarist { get; set; }
        [Display(Name = "Dessinateur")]
        public string Cartoonist { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(0, 10)]
        [Display(Name = "Note")]
        public int Rating { get; set; }
        public string UserId { get; set; }
    }
}