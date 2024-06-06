namespace Projekt_Koncowy.Data.DTOs;

public class WizytaDto
{
    public int IdWizyty { get; set; }
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
    public DoktorDto Doktor { get; set; } = null!;
    public object Pacjent { get; set; } = null!; // Może to być DoroslyWithIdDto, DzieckoWithIdDto lub SeniorWithIdDto
    public PlacowkaDto Placowka { get; set; } = null!;
}