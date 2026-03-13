using dashuyg.Data;
using dashuyg.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dashuyg.Controllers;

[ApiController]
[Route("api/ogrenciler")]
public class ModelApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public ModelApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<OgrenciListDto>>> GetAll()
    {
        var list = await _context.Ogrenciler
            .Include(o => o.Sinif)
            .Select(o => new OgrenciListDto
            {
                Id = o.Id,
                Ad = o.Ad,
                Soyad = o.Soyad,
                Yas = o.Yas,
                SinifId = o.SinifId,
                SinifAd = o.Sinif != null ? o.Sinif.Ad : string.Empty
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OgrenciCrudDto>> GetById(int id)
    {
        var ogrenci = await _context.Ogrenciler.FindAsync(id);
        if (ogrenci == null) return NotFound();

        var dto = new OgrenciCrudDto
        {
            Id = ogrenci.Id,
            Ad = ogrenci.Ad,
            Soyad = ogrenci.Soyad,
            Yas = ogrenci.Yas,
            SinifId = ogrenci.SinifId
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<OgrenciCrudDto>> Create([FromBody] OgrenciCrudDto dto)
    {
        var sinifVarMi = await _context.Siniflar.AnyAsync(s => s.Id == dto.SinifId);
        if (!sinifVarMi) return BadRequest("SinifId gecersiz.");

        var ogrenci = new Ogrenci
        {
            Ad = dto.Ad,
            Soyad = dto.Soyad,
            Yas = dto.Yas,
            SinifId = dto.SinifId
        };

        _context.Ogrenciler.Add(ogrenci);
        await _context.SaveChangesAsync();

        dto.Id = ogrenci.Id;
        return CreatedAtAction(nameof(GetById), new { id = ogrenci.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] OgrenciCrudDto dto)
    {
        if (id != dto.Id) return BadRequest("Id uyusmuyor.");

        var ogrenci = await _context.Ogrenciler.FindAsync(id);
        if (ogrenci == null) return NotFound();

        var sinifVarMi = await _context.Siniflar.AnyAsync(s => s.Id == dto.SinifId);
        if (!sinifVarMi) return BadRequest("SinifId gecersiz.");

        ogrenci.Ad = dto.Ad;
        ogrenci.Soyad = dto.Soyad;
        ogrenci.Yas = dto.Yas;
        ogrenci.SinifId = dto.SinifId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ogrenci = await _context.Ogrenciler.FindAsync(id);
        if (ogrenci == null) return NotFound();

        _context.Ogrenciler.Remove(ogrenci);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
