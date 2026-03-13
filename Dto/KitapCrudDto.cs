namespace dashuyg.Dto;

public class KitapCrudDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Yazar { get; set; } = string.Empty;
    public int SayfaSayisi { get; set; }
    public int KategoriId { get; set; }
}
