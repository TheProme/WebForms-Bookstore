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
        [Required]
        public string Title { get; set; }
        [Required]
        public string About { get; set; }
        [Required]
        public Author Author { get; set; }
        [Required]
        public Genre Genre { get; set; }
    }
}