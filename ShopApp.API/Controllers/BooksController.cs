using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.API.Data;
using ShopApp.API.Dtos;
using ShopApp.API.Models;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookShopDbContext _context;
        private readonly IMapper _mapper;

        public BooksController(BookShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
          if (_context.Books == null)
           return NotFound();
            var books = await _context.Books
                .Include(p => p.Author)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(books);
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
          if (_context.Books == null)
              return NotFound();

            var book = await _context.Books
                .Include(p => p.Author)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto req)
        {
            if (id != req.Id)
                return BadRequest();

            var book = await _context.Books.FindAsync(id);
            _mapper.Map(req, book);
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDto>> PostBook(BookCreateDto req)
        {
          if (_context.Books == null)
              return Problem("Entity set 'BookShopDbContext.Books'  is null.");

            var book = _mapper.Map<Book>(req);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var bookDto = _mapper.Map<BookDto>(book);

            return CreatedAtAction("GetBook", new { id = book.Id }, bookDto);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> BookExists(int id) => await _context.Books?.AnyAsync(e => e.Id == id);
    }
}
