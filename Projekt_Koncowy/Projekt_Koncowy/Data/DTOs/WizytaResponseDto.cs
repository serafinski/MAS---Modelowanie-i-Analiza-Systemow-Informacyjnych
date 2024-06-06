namespace Projekt_Koncowy.Data.DTOs;

public class WizytaResponseDto
{
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
    public DoktorDto Doktor { get; set; } = null!;
    public object Pacjent { get; set; } = null!; // Może to być DoroslyDto, DzieckoDto lub SeniorDto
    public PlacowkaDto Placowka { get; set; } = null!;
}