using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
    }
}