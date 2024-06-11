using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Context;
using WebApi.Models.Books;
using WebApi.Models.Requests;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }
        [HttpPost("AddBook")]
        [Authorize(Roles ="Admin")]
        public ActionResult<int> AddBook([FromBody] AddBookRequest request)
        {
            var book = new Book
            {
                Description = request.Description,
                Price = request.Price,
                Count = request.Count,
                Title = request.Title,
            };
            _context.Add(book);
            _context.SaveChanges();
            return book.Id;    
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBook([FromRoute] int id)
        {
            var book=_context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound();

            var result = _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<int> ChangeBook([FromRoute]int id,[FromBody] ChangeBookRequest request)
        {
            if (id != request.Id)
                return BadRequest();
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if(book == null)
                return NotFound();
            _context.Books.Update(book);
            _context.SaveChanges();
            return book.Id;
        }


        [HttpPost("Buy")]
        [Authorize(Roles = "Consumer")]
        public ActionResult<string> Buy([FromBody] BuyBookRequest request)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == request.Id);
            if(book == null)
                return NotFound();
            book.RemoveFromStorage();
            _context.Books.Update(book);
            _context.SaveChanges();
            return book.Title;
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<BookDTO> Get([FromRoute] int id)
        {
           var book= _context.Books.AsNoTracking().FirstOrDefault(x=>x.Id == id);
            if (book == null)
                return NotFound();
            var result = new BookDTO(book);
            return result;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<BookDTO>> GetAll()
        {
            var books = _context.Books.AsNoTracking().ToList();
            if (books == null)
                return NotFound();
            var result = books.Select(x => new BookDTO(x)).ToList(); 
            return result;
        }
    }
}
