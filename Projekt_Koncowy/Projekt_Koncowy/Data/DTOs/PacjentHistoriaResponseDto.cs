namespace Projekt_Koncowy.Data.DTOs;

public class PacjentHistoriaResponseDto
{
    public object Pacjent { get; set; } = null!; // Może to być DoroslyDto, DzieckoDto lub SeniorDto
    public List<WizytaHistoriaDodajDto> Wizyty { get; set; } = new List<WizytaHistoriaDodajDto>();
}