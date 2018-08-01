using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreasureBooks.Models
{
    public class BookDetailDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
    }
}