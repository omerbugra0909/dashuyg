using System.ComponentModel.DataAnnotations;

public class Kategori
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı gereklidir.")]
    [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
    public string Ad { get; set; } = string.Empty;

    public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

    public ICollection<Kitap> Kitaplar { get; set; } = new List<Kitap>();
}

