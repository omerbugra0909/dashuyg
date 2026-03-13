using System.ComponentModel.DataAnnotations;

public class Sinif
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Sınıf adı gereklidir")]
    public string Ad { get; set; } = string.Empty;

    public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

    public ICollection<Ogrenci> Ogrenciler { get; set; } = new List<Ogrenci>();
}