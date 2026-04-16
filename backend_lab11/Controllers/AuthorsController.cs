using backend_lab11.Data;
using backend_lab11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_lab11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AuthorsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAll()
    {
        var authors = await _db.Authors
            .Include(a => a.Books)
                .ThenInclude(b => b.Publisher)
            .AsNoTracking()
            .ToListAsync();

        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetById(int id)
    {
        var author = await _db.Authors
            .Include(a => a.Books)
                .ThenInclude(b => b.Publisher)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return author is null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> Create(Author author)
    {
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Author input)
    {
        var author = await _db.Authors.FindAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        author.Name = input.Name;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _db.Authors.FindAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        _db.Authors.Remove(author);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}