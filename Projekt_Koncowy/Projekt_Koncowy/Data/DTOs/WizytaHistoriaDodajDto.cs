namespace Projekt_Koncowy.Data.DTOs;

public class WizytaHistoriaDodajDto
{
    public int IdWizyty { get; set; }
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
    public DoktorDto Doktor { get; set; } = null!;
    public PlacowkaDto Placowka { get; set; } = null!;
}