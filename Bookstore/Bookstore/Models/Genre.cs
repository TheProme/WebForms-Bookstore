using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required genre name")]
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}