using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreasureBooks.Models;
using TreasureBooks.Controllers;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;
using System.Linq;

namespace TreasureBooks.Tests
{
    [TestClass]
    public class TestBookController
    {
        [TestMethod]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            var controller = new BooksController(GetTestBooks());
            IQueryable<BookDTO> result = controller.GetBooks();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public async Task DeleteBookAsync_ShouldReturnOK()
        {
            var context = new TestContext();
            var item = GetTestBook();
            context.Books.Add(item);

            var controller = new BooksController(context);
            var result = await controller.DeleteBook(3) as OkNegotiatedContentResult<Book>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.BookId, result.Content.BookId);
        }

        [TestMethod]
        public async Task GetBook_ShouldReturnBookWithSameID()
        {
            var context = new TestContext();
            context.Books.Add(GetTestBook());
            context.Authors.Add(new Author { AuthorId = 2, Name = "Charles Dickens" });

            var controller = new BooksController(context);
            var result = await controller.GetBook(3) as OkNegotiatedContentResult<BookDetailDTO>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.BookId);
        }

        [TestMethod]
        public void GetBook_ShouldNotFindBook()
        {
            var controller = new BooksController(GetTestBooks());
            var result = controller.GetBook(999);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostBook_ShouldReturnSameBook()
        {
            var context = new TestContext();
            var item = GetTestBook();

            var controller = new BooksController(context);
            var actionResult = await controller.PostBook(item) as IHttpActionResult;
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<BookDTO>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(item.BookId, createdResult.RouteValues["id"]);
            Assert.AreEqual(item.Title, createdResult.Content.Title);
        }

        [TestMethod]
        public async Task PutBook_ReturnsContentResult()
        {
            var context = new TestContext();
            var item = GetTestBook();

            var controller = new BooksController(context);
            var actionResult = await controller.PutBook(item.BookId, item) as IHttpActionResult;
            var contentResult = actionResult as NegotiatedContentResult<Book>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(item.BookId, contentResult.Content.BookId);
            Assert.AreEqual(item.Title, contentResult.Content.Title);
        }

        Book GetTestBook()
        {
            return new Book() {
                BookId = 3,
                Title = "David Copperfield",
                Price = 15,
                Genre = "Bildungsroman",
                AuthorId = 2,
                Author = new Author() { AuthorId = 2, Name = "Charles Dickens" }
            };
        }

        private ITreasureBooksContext GetTestBooks()
        {
            var context = new TestContext();

            context.Books.Add(new Book(){
                BookId = 1,
                Title = "Pride and Prejudice",
                Price = 9.99M,
                Genre = "Comedy of manners",
                AuthorId = 1,
                Author = new Author() { AuthorId = 1, Name = "Jane Austen" }
            });

            context.Books.Add(new Book()
            {
                BookId = 2,
                Title = "Northanger Abbey",
                Price = 12.95M,
                Genre = "Gothic parody",
                AuthorId = 1,
                Author = new Author() { AuthorId = 1, Name = "Jane Austen" }
            });

            context.Books.Add(new Book()
            {
                BookId = 3,
                Title = "David Copperfield",
                Price = 15,
                Genre = "Bildungsroman",
                AuthorId = 2,
                Author = new Author() { AuthorId = 2, Name = "Charles Dickens" }
            });

            context.Books.Add(new Book()
            {
                BookId = 4,
                Title = "Don Quixote",
                Price = 8.95M,
                Genre = "Picaresque",
                AuthorId = 3,
                Author = new Author() { AuthorId = 3, Name = "Miguel de Cervantes" }
            });

            context.Authors.Add(new Author { AuthorId = 1, Name = "Jane Austen" });
            context.Authors.Add(new Author { AuthorId = 2, Name = "Charles Dickens" });
            context.Authors.Add(new Author { AuthorId = 3, Name = "Miguel de Cervantes" });

            return context;
        }
    }
}
