using System.ComponentModel.DataAnnotations;

namespace TreasureBooks.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }


        // Foreign Key
        public int AuthorId { get; set; }
        // Navigation property
        public Author Author { get; set; }
    }
}