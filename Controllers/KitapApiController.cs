using dashuyg.Data;
using dashuyg.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dashuyg.Controllers;

[ApiController]
[Route("api/kitaplar")]
public class KitapApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public KitapApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<KitapListDto>>> GetAll()
    {
        var list = await _context.Kitaplar
            .Include(k => k.Kategori)
            .Select(k => new KitapListDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Yazar = k.Yazar,
                SayfaSayisi = k.SayfaSayisi,
                KategoriId = k.KategoriId,
                KategoriAd = k.Kategori != null ? k.Kategori.Ad : string.Empty
            })
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<KitapCrudDto>> GetById(int id)
    {
        var kitap = await _context.Kitaplar.FindAsync(id);
        if (kitap == null) return NotFound();

        var dto = new KitapCrudDto
        {
            Id = kitap.Id,
            Ad = kitap.Ad,
            Yazar = kitap.Yazar,
            SayfaSayisi = kitap.SayfaSayisi,
            KategoriId = kitap.KategoriId
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<KitapCrudDto>> Create([FromBody] KitapCrudDto dto)
    {
        var kategoriVarMi = await _context.Kategoriler.AnyAsync(k => k.Id == dto.KategoriId);
        if (!kategoriVarMi) return BadRequest("KategoriId gecersiz.");

        var kitap = new Kitap
        {
            Ad = dto.Ad,
            Yazar = dto.Yazar,
            SayfaSayisi = dto.SayfaSayisi,
            KategoriId = dto.KategoriId
        };

        _context.Kitaplar.Add(kitap);
        await _context.SaveChangesAsync();

        dto.Id = kitap.Id;
        return CreatedAtAction(nameof(GetById), new { id = kitap.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] KitapCrudDto dto)
    {
        if (id != dto.Id) return BadRequest("Id uyusmuyor.");

        var kitap = await _context.Kitaplar.FindAsync(id);
        if (kitap == null) return NotFound();

        var kategoriVarMi = await _context.Kategoriler.AnyAsync(k => k.Id == dto.KategoriId);
        if (!kategoriVarMi) return BadRequest("KategoriId gecersiz.");

        kitap.Ad = dto.Ad;
        kitap.Yazar = dto.Yazar;
        kitap.SayfaSayisi = dto.SayfaSayisi;
        kitap.KategoriId = dto.KategoriId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var kitap = await _context.Kitaplar.FindAsync(id);
        if (kitap == null) return NotFound();

        _context.Kitaplar.Remove(kitap);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
