using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using dashuyg.Data;
using dashuyg.Models;

namespace dashuyg.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly AppDbContext _context;

        public OgrenciController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler
                                           .Include(o => o.Sinif)
                                           .ToListAsync();
            return View(ogrenciler);
        }

        public async Task<IActionResult> Ekle()
        {
            ViewBag.Siniflar = new SelectList(await _context.Siniflar.ToListAsync(), "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Siniflar = new SelectList(await _context.Siniflar.ToListAsync(), "Id", "Ad");
                return View(ogrenci);
            }

            try
            {
                _context.Ogrenciler.Add(ogrenci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Öğrenci eklenirken hata oluştu: " + ex.Message);
                ViewBag.Siniflar = new SelectList(await _context.Siniflar.ToListAsync(), "Id", "Ad");
                return View(ogrenci);
            }
        }

        public async Task<IActionResult> Guncelle(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null) return NotFound();

            ViewBag.Siniflar = new SelectList(await _context.Siniflar.ToListAsync(), "Id", "Ad", ogrenci.SinifId);
            return View(ogrenci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Siniflar = new SelectList(await _context.Siniflar.ToListAsync(), "Id", "Ad", ogrenci.SinifId);
                return View(ogrenci);
            }

            var eski = await _context.Ogrenciler.FindAsync(ogrenci.Id);
            if (eski == null) return NotFound();

            eski.Ad = ogrenci.Ad;
            eski.Soyad = ogrenci.Soyad;
            eski.Yas = ogrenci.Yas;
            eski.SinifId = ogrenci.SinifId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Sil(int id)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null) return NotFound();

            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
