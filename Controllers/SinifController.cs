using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dashuyg.Data;
using dashuyg.Models;

namespace dashuyg.Controllers
{
    public class SinifController : Controller
    {
        private readonly AppDbContext _context;

        public SinifController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var siniflar = await _context.Siniflar
                                         .Include(s => s.Ogrenciler)
                                         .ToListAsync();
            return View(siniflar);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(Sinif sinif)
        {
            if (!ModelState.IsValid) return View(sinif);

            _context.Siniflar.Add(sinif);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Guncelle(int id)
        {
            var sinif = await _context.Siniflar.FindAsync(id);
            if (sinif == null) return NotFound();
            return View(sinif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(Sinif sinif)
        {
            if (!ModelState.IsValid) return View(sinif);

            var eski = await _context.Siniflar.FindAsync(sinif.Id);
            if (eski == null) return NotFound();

            eski.Ad = sinif.Ad;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Sil(int id)
        {
            var sinif = await _context.Siniflar.FindAsync(id);
            if (sinif == null) return NotFound();

            _context.Siniflar.Remove(sinif);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
