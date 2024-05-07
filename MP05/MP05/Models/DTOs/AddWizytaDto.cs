namespace MP05.Models.DTOs;

public class AddWizytaDto
{
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
    public int IdDoktor { get; set; }
}