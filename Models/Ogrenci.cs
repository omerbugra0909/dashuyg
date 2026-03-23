using System.ComponentModel.DataAnnotations;

public class Ogrenci
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad gereklidir.")]
    public string Ad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad gereklidir.")]
    public string Soyad { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yaş gereklidir.")]
    [Range(1, 100, ErrorMessage = "Yaş 1-100 arasında olmalıdır.")]
    public int Yas { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Sınıf seçimi gereklidir.")]
    public int SinifId { get; set; }

    public Sinif? Sinif { get; set; }
}




















































