using backend_lab11.Data;
using backend_lab11.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_lab11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly AppDbContext _db;

    public PublishersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetAll()
    {
        var publishers = await _db.Publishers
            .Include(p => p.Books)
                .ThenInclude(b => b.Author)
            .AsNoTracking()
            .ToListAsync();

        return Ok(publishers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Publisher>> GetById(int id)
    {
        var publisher = await _db.Publishers
            .Include(p => p.Books)
                .ThenInclude(b => b.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return publisher is null ? NotFound() : Ok(publisher);
    }

    [HttpPost]
    public async Task<ActionResult<Publisher>> Create(Publisher publisher)
    {
        _db.Publishers.Add(publisher);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = publisher.Id }, publisher);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Publisher input)
    {
        var publisher = await _db.Publishers.FindAsync(id);
        if (publisher is null)
        {
            return NotFound();
        }

        publisher.Name = input.Name;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var publisher = await _db.Publishers.FindAsync(id);
        if (publisher is null)
        {
            return NotFound();
        }

        _db.Publishers.Remove(publisher);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}