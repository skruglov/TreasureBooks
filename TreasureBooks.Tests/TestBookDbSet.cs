using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureBooks.Models;

namespace TreasureBooks.Tests
{
    class TestBookDbSet : TestDbSet<Book>
    {
        public override Book Find(params object[] keyValues)
        {
            return this.SingleOrDefault(book => book.BookId == (int)keyValues.Single());
        }

        public override Task<Book> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }
}
