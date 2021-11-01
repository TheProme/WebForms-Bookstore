using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required title")]
        public string Title { get; set; }
        public string About { get; set; }
        [Required(ErrorMessage = "Required author")]
        public Author Author { get; set; }
        [Required(ErrorMessage = "Required genre")]
        public Genre Genre { get; set; }
    }
}