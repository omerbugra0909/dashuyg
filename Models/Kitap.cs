using System.ComponentModel.DataAnnotations;

public class Kitap
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kitap adı gereklidir.")]
    public string Ad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yazar adı gereklidir.")]
    public string Yazar { get; set; } = string.Empty;

    [Required(ErrorMessage = "Sayfa sayısı gereklidir.")]
    [Range(1, int.MaxValue, ErrorMessage = "Sayfa sayısı 1'den büyük olmalıdır.")]
    public int SayfaSayisi { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Kategori seçimi gereklidir.")]
    public int KategoriId { get; set; }
    public Kategori? Kategori { get; set; }
}
