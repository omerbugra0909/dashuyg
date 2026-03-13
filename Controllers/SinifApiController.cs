using dashuyg.Data;
using dashuyg.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dashuyg.Controllers;

[ApiController]
[Route("api/siniflar")]
public class SinifApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public SinifApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SinifListDto>>> GetAll()
    {
        var list = await _context.Siniflar
            .Include(s => s.Ogrenciler)
            .Select(s => new SinifListDto
            {
                Id = s.Id,
                Ad = s.Ad,
                OlusturmaTarihi = s.OlusturmaTarihi,
                OgrenciSayisi = s.Ogrenciler.Count
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SinifCrudDto>> GetById(int id)
    {
        var sinif = await _context.Siniflar.FindAsync(id);
        if (sinif == null) return NotFound();

        var dto = new SinifCrudDto
        {
            Id = sinif.Id,
            Ad = sinif.Ad
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<SinifCrudDto>> Create([FromBody] SinifCrudDto dto)
    {
        var sinif = new Sinif
        {
            Ad = dto.Ad,
            OlusturmaTarihi = DateTime.Now
        };

        _context.Siniflar.Add(sinif);
        await _context.SaveChangesAsync();

        dto.Id = sinif.Id;
        return CreatedAtAction(nameof(GetById), new { id = sinif.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SinifCrudDto dto)
    {
        if (id != dto.Id) return BadRequest("Id uyusmuyor.");

        var sinif = await _context.Siniflar.FindAsync(id);
        if (sinif == null) return NotFound();

        sinif.Ad = dto.Ad;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sinif = await _context.Siniflar
            .Include(s => s.Ogrenciler)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sinif == null) return NotFound();
        if (sinif.Ogrenciler.Any()) return BadRequest("Sinifa bagli ogrenciler var.");

        _context.Siniflar.Remove(sinif);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
