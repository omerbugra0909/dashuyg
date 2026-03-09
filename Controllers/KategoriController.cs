using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dashuyg.Data;
using dashuyg.Models;

namespace dashuyg.Controllers
{
    public class KategoriController : Controller
    {
        private readonly AppDbContext _context;

        public KategoriController(AppDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var kategoriler = await _context.Kategoriler
                                            .Include(k => k.Kitaplar)
                                            .ToListAsync();
            return View(kategoriler);
        }

        // CREATE
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(Kategori kategori)
        {
            if (!ModelState.IsValid) return View(kategori);

            _context.Kategoriler.Add(kategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // UPDATE
        public async Task<IActionResult> Guncelle(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null) return NotFound();
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(Kategori kategori)
        {
            if (!ModelState.IsValid) return View(kategori);

            var eski = await _context.Kategoriler.FindAsync(kategori.Id);
            if (eski == null) return NotFound();

            eski.Ad = kategori.Ad;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Sil(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null) return NotFound();

            _context.Kategoriler.Remove(kategori);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}