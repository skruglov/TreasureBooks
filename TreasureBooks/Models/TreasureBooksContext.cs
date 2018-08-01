using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TreasureBooks.Models
{
    public class TreasureBooksContext : DbContext, ITreasureBooksContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TreasureBooksContext() : base("name=TreasureBooksContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<TreasureBooks.Models.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<TreasureBooks.Models.Book> Books { get; set; }

        public void MarkAsModified(Author item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsModified(Book item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void LoadAuthorName(Book book)
        {
            Entry(book).Reference(x => x.Author).Load();
        }
    }
}
