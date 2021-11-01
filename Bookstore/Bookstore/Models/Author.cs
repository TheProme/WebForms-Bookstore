using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required author name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required author surname")]
        public string Surname { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}