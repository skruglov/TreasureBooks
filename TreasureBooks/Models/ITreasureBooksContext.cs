using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;


namespace TreasureBooks.Models
{
    public interface ITreasureBooksContext : IDisposable
    {
        DbSet<Author> Authors { get; }
        DbSet<Book> Books { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void MarkAsModified(Author item);
        void MarkAsModified(Book item);
        void LoadAuthorName(Book book);
    }
}