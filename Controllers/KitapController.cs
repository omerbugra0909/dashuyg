using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dashuyg.Data;
using dashuyg.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dashuyg.Controllers
{
    public class KitapController : Controller
    {
        private readonly AppDbContext _context;

        public KitapController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var kitaplar = await _context.Kitaplar
                                         .Include(k => k.Kategori)
                                         .ToListAsync();
            return View(kitaplar);
        }

        
        public async Task<IActionResult> Ekle()
        {
            ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(Kitap kitap)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "Id", "Ad");
                return View(kitap);
            }

            try
            {
                _context.Kitaplar.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kitap eklenirken hata oluştu: " + ex.Message);
                ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "Id", "Ad");
                return View(kitap);
            }
        }

      
        public async Task<IActionResult> Guncelle(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();

            ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "Id", "Ad", kitap.KategoriId);
            return View(kitap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(Kitap kitap)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "Id", "Ad", kitap.KategoriId);
                return View(kitap);
            }

            var eski = await _context.Kitaplar.FindAsync(kitap.Id);
            if (eski == null) return NotFound();

            eski.Ad = kitap.Ad;
            eski.Yazar = kitap.Yazar;
            eski.SayfaSayisi = kitap.SayfaSayisi;
            eski.KategoriId = kitap.KategoriId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Sil(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();

            _context.Kitaplar.Remove(kitap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
