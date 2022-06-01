using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.API.Data;
using ShopApp.API.Dtos;
using ShopApp.API.Models;
using ShopApp.API.Static;

namespace ShopApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly BookShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AuthorsController(BookShopDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorGetDto>>> GetAuthors()
        {
            try
            {
                if (_context.Authors == null)
                    return NotFound();
                var authors = await _context.Authors.ToListAsync();
                var authorDtos = _mapper.Map<IEnumerable<AuthorGetDto>>(authors);
                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured at method {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorGetDto>> GetAuthor(int id)
        {
            try
            {
                if (_context.Authors == null)
                    return NotFound();

                var author = await _context.Authors.FindAsync(id);
                var authorDtos = _mapper.Map<AuthorGetDto>(author);

                if (authorDtos == null)
                    return NotFound();
                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured at method {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto request)
        {
            if (id != request.Id)
                return BadRequest();

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            _mapper.Map(request, author);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (! await AuthorExists(id))
                    return NotFound();
                else
                {
                    _logger.LogError(ex, $"Error occured at method {nameof(PutAuthor)}");
                    return StatusCode(500, Messages.Error500);
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorGetDto>> PostAuthor(AuthorCreateDto request)
        {
            try
            {
                if (_context.Authors == null)
                    return Problem("Entity set 'BookShopDbContext.Authors'  is null.");

                var author = _mapper.Map<Author>(request);
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured at method {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                if (_context.Authors == null)
                    return NotFound();
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                    return NotFound();

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured at method {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500);
            }
        }

        private async Task<bool> AuthorExists(int id) => await _context.Authors?.AnyAsync(e => e.Id == id);
    }
}
