namespace dashuyg.Dto;

public class OgrenciCrudDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;
    public int Yas { get; set; }
    public int SinifId { get; set; }
}
