namespace Projekt_Koncowy.Data.DTOs;

public class OddzialResponseDto
{
    public int IdOddzial { get; set; }
    public string NazwaOddzial { get; set; } = null!;
    public int IdPlacowki { get; set; }
    public string NazwaPlacowki { get; set; } = null!;
}