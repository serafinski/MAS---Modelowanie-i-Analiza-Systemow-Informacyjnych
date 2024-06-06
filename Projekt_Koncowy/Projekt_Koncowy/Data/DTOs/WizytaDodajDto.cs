namespace Projekt_Koncowy.Data.DTOs;

public class WizytaDodajDto
{
    public int IdPacjent { get; set; }
    public int IdDoktor { get; set; }
    public string OpisWizyty { get; set; } = null!;
    public DateTime DataWizyty { get; set; }
    public int IdPlacowka { get; set; }
}