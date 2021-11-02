using System.ComponentModel.DataAnnotations;

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