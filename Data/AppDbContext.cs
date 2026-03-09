using Microsoft.EntityFrameworkCore;
using dashuyg.Models;

namespace dashuyg.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Kategori>().HasData(
                new Kategori { Id = 1, Ad = "Roman" },
                new Kategori { Id = 2, Ad = "Bilim" },
                new Kategori { Id = 3, Ad = "Tarih" }
            );

            modelBuilder.Entity<Kitap>().HasData(
                new Kitap { Id = 1, Ad = "Suç ve Ceza", Yazar = "Dostoyevski", SayfaSayisi = 600, KategoriId = 1 },
                new Kitap { Id = 2, Ad = "1984", Yazar = "George Orwell", SayfaSayisi = 328, KategoriId = 1 }
            );
        }
    }
}