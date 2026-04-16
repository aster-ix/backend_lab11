using backend_lab11.Data;
using backend_lab11.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_lab11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _db;

    public BooksController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _db.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .AsNoTracking()
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _db.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book input)
    {
        var authorExists = await _db.Authors.AnyAsync(x => x.Id == input.AuthorId);
        var publisherExists = await _db.Publishers.AnyAsync(x => x.Id == input.PublisherId);

        if (!authorExists || !publisherExists)
        {
            return BadRequest("Автор или издатель не найдены.");
        }

        _db.Books.Add(input);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Book input)
    {
        var book = await _db.Books.FindAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        var authorExists = await _db.Authors.AnyAsync(x => x.Id == input.AuthorId);
        var publisherExists = await _db.Publishers.AnyAsync(x => x.Id == input.PublisherId);

        if (!authorExists || !publisherExists)
        {
            return BadRequest("Автор или издатель не найдены.");
        }

        book.Title = input.Title;
        book.Year = input.Year;
        book.AuthorId = input.AuthorId;
        book.PublisherId = input.PublisherId;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book is null)
        {
            return NotFound();
        }

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}