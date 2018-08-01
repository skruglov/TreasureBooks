using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TreasureBooks.Models;

namespace TreasureBooks.Tests
{
    public class TestContext : ITreasureBooksContext
    {
        public TestContext()
        {
            this.Authors = new TestAuthorDbSet();
            this.Books = new TestBookDbSet();
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(SaveChanges());
        }

        public void MarkAsModified(Author item) { }

        public void MarkAsModified(Book item) { }

        public void LoadAuthorName(Book book) { }

        public void Dispose() { }
    }
}
