namespace MP05.Models.DTOs;

public class GetDoktorWizytaDto
{
    public int IdWizyta { get; set; }
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
}