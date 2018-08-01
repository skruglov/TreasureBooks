using System;
using System.Linq;
using TreasureBooks.Models;
using System.Threading.Tasks;

namespace TreasureBooks.Tests
{
    class TestAuthorDbSet : TestDbSet<Author>
    {
        public override Author Find(params object[] keyValues)
        {
            return this.SingleOrDefault(author => author.AuthorId == (int)keyValues.Single());
        }

        public override Task<Author> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }
}