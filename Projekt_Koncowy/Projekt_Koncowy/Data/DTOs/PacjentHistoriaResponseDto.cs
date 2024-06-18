namespace Projekt_Koncowy.Data.DTOs;

public class PacjentHistoriaResponseDto
{
    // Może to być DoroslyDto, DzieckoDto lub SeniorDto
    public object Pacjent { get; set; } = null!; 
    public List<WizytaHistoriaDodajDto> Wizyty { get; set; } = new List<WizytaHistoriaDodajDto>();
}