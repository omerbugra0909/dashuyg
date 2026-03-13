using dashuyg.Data;
using dashuyg.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dashuyg.Controllers;

[ApiController]
[Route("api/kategoriler")]
public class KategoriApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public KategoriApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<KategoriListDto>>> GetAll()
    {
        var list = await _context.Kategoriler
            .Include(k => k.Kitaplar)
            .Select(k => new KategoriListDto
            {
                Id = k.Id,
                Ad = k.Ad,
                OlusturmaTarihi = k.OlusturmaTarihi,
                KitapSayisi = k.Kitaplar.Count
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<KategoriCrudDto>> GetById(int id)
    {
        var kategori = await _context.Kategoriler.FindAsync(id);
        if (kategori == null) return NotFound();

        var dto = new KategoriCrudDto
        {
            Id = kategori.Id,
            Ad = kategori.Ad
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<KategoriCrudDto>> Create([FromBody] KategoriCrudDto dto)
    {
        var kategori = new Kategori
        {
            Ad = dto.Ad,
            OlusturmaTarihi = DateTime.Now
        };

        _context.Kategoriler.Add(kategori);
        await _context.SaveChangesAsync();

        dto.Id = kategori.Id;
        return CreatedAtAction(nameof(GetById), new { id = kategori.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] KategoriCrudDto dto)
    {
        if (id != dto.Id) return BadRequest("Id uyusmuyor.");

        var kategori = await _context.Kategoriler.FindAsync(id);
        if (kategori == null) return NotFound();

        kategori.Ad = dto.Ad;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var kategori = await _context.Kategoriler
            .Include(k => k.Kitaplar)
            .FirstOrDefaultAsync(k => k.Id == id);

        if (kategori == null) return NotFound();
        if (kategori.Kitaplar.Any()) return BadRequest("Kategoriye bagli kitaplar var.");

        _context.Kategoriler.Remove(kategori);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
