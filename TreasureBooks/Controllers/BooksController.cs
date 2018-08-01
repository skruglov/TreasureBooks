using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TreasureBooks.Models;

namespace TreasureBooks.Controllers
{
    public class BooksController : ApiController
    {
        private ITreasureBooksContext db = new TreasureBooksContext();

        public BooksController()
        {
        }

        public BooksController(ITreasureBooksContext context)
        {
            db = context;
        }

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            var books = from b in db.Books
                        select new BookDTO()
                        {
                            BookId = b.BookId,
                            Title = b.Title,
                            AuthorName = b.Author.Name
                        };

            return books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(BookDetailDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            Author author = await db.Authors.FindAsync(book.AuthorId);
            if (author == null)
            {
                return NotFound();
            }

            BookDetailDTO result = new BookDetailDTO()
            {
                BookId = book.BookId,
                Title = book.Title,
                Price = book.Price,
                AuthorId = book.AuthorId,
                AuthorName = author.Name,
                Genre = book.Genre
            };

            return Ok(result);
        }
        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                return BadRequest();
            }

            db.MarkAsModified(book);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content(HttpStatusCode.Accepted, book);
        }

        [ResponseType(typeof(BookDTO))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();

            // Load author name
            //db.Entry(book).Reference(x => x.Author).Load();
            db.LoadAuthorName(book);

            var dto = new BookDTO()
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = book.Author.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = book.BookId }, dto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}