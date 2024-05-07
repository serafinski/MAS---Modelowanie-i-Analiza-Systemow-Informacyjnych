namespace MP05.Models.DTOs;

public class WizytaResponseDto
{
    public int IdWizyta { get; set; }
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; }
    public int IdDoktor { get; set; }
}