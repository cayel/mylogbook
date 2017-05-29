using System;
using System.ComponentModel.DataAnnotations;

namespace MyLogbook.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Auteur")]
        public string Writer { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Range(0, 10)]
        [Display(Name = "Note")]
        public int Rating { get; set; }
        public string UserId { get; set; }
    }
}