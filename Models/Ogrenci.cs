using System.ComponentModel.DataAnnotations;

public class Ogrenci
{
    public int Id { get; set; }

    [Required]
    public string Ad { get; set; } = string.Empty;

    public string Soyad { get; set; } = string.Empty;

    public int Yas { get; set; }

    public int SinifId { get; set; }

    public Sinif Sinif { get; set; }
}





















































