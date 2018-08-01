namespace TreasureBooks.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TreasureBooks.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TreasureBooks.Models.TreasureBooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TreasureBooks.Models.TreasureBooksContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Authors.AddOrUpdate(x => x.AuthorId,
                new Author() { AuthorId = 1, Name = "Jane Austen" },
                new Author() { AuthorId = 2, Name = "Charles Dickens" },
                new Author() { AuthorId = 3, Name = "Miguel de Cervantes" }
                );

            context.Books.AddOrUpdate(x => x.BookId,
                new Book()
                {
                    BookId = 1,
                    Title = "Pride and Prejudice",
                    AuthorId = 1,
                    Price = 9.99M,
                    Genre = "Comedy of manners"
                },
                new Book()
                {
                    BookId = 2,
                    Title = "Northanger Abbey",
                    AuthorId = 1,
                    Price = 12.95M,
                    Genre = "Gothic parody"
                },
                new Book()
                {
                    BookId = 3,
                    Title = "David Copperfield",
                    AuthorId = 2,
                    Price = 15,
                    Genre = "Bildungsroman"
                },
                new Book()
                {
                    BookId = 4,
                    Title = "Don Quixote",
                    AuthorId = 3,
                    Price = 8.95M,
                    Genre = "Picaresque"
                }
                );
        }
    }
}
