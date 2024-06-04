namespace Projekt_Koncowy.Data.DTOs;

public class PielegniarkaResponseDto
{
    public int IdOsoba { get; set; }
    public int IdPielegniarka { get; set; }
    public string Nazwisko { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public List<string> Imiona { get; set; } = new();
    public string Adres { get; set; } = null!;
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    public string Grafik { get; set; } = null!;
}