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

namespace TreasureBooks.Tests
{
    [TestClass]
    public class TestAuthorsController
    {
        [TestMethod]
        public void GetAllAuthors_ShouldReturnAllAuthors()
        {
            var controller = new AuthorsController(GetTestAuthors());
            var result = controller.GetAuthors() as TestAuthorDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public async Task DeleteAuthorAsync_ShouldReturnOK()
        {
            var context = new TestContext();
            var item = GetTestAuthor();
            context.Authors.Add(item);

            var controller = new AuthorsController(context);
            var result = await controller.DeleteAuthor(3) as OkNegotiatedContentResult<Author>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.AuthorId, result.Content.AuthorId);
        }

        [TestMethod]
        public async Task GetAuthor_ShouldReturnAuthorWithSameID()
        {
            var context = new TestContext();
            context.Authors.Add(GetTestAuthor());

            var controller = new AuthorsController(context);
            var result = await controller.GetAuthor(3) as OkNegotiatedContentResult<Author>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.AuthorId);
        }

        [TestMethod]
        public void GetAuthor_ShouldNotFindAuthor()
        {
            var controller = new AuthorsController(GetTestAuthors());
            var result = controller.GetAuthor(999);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostAuthor_ShouldReturnSameAuthor()
        {
            var context = new TestContext();
            var item = GetTestAuthor();

            var controller = new AuthorsController(context);
            var actionResult = await controller.PostAuthor(item) as IHttpActionResult;
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Author>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(item.AuthorId, createdResult.RouteValues["id"]);
            Assert.AreEqual(item.Name, createdResult.Content.Name);
        }

        [TestMethod]
        public async Task PutAuthor_ReturnsContentResult()
        {
            var context = new TestContext();
            var item = GetTestAuthor();

            var controller = new AuthorsController(context);
            var actionResult = await controller.PutAuthor(item.AuthorId, item) as IHttpActionResult;
            var contentResult = actionResult as NegotiatedContentResult<Author>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(item.AuthorId, contentResult.Content.AuthorId);
            Assert.AreEqual(item.Name, contentResult.Content.Name);
        }

        Author GetTestAuthor()
        {
            return new Author() { AuthorId = 3, Name = "Miguel de Cervantes" };
        }

        private ITreasureBooksContext GetTestAuthors()
        {
            var context = new TestContext();
            context.Authors.Add(new Author { AuthorId = 1, Name = "Jane Austen" });
            context.Authors.Add(new Author { AuthorId = 2, Name = "Charles Dickens" });
            context.Authors.Add(new Author { AuthorId = 3, Name = "Miguel de Cervantes" });

            return context;
        }
    }
}
