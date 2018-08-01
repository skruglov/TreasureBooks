using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreasureBooks.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}