namespace dashuyg.Dto;

public class KitapListDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Yazar { get; set; } = string.Empty;
    public int SayfaSayisi { get; set; }
    public int KategoriId { get; set; }
    public string KategoriAd { get; set; } = string.Empty;
}
