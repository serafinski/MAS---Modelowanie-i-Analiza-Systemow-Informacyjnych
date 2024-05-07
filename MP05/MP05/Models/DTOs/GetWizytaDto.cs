namespace MP05.Models.DTOs;

public class GetWizytaDto
{
    public int IdWizyta { get; set; }
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
    public int IdDoktor { get; set; }
}