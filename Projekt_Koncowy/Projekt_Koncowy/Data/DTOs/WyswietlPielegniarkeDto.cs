namespace Projekt_Koncowy.Data.DTOs;

public class WyswietlPielegniarkeDto
{
    public int IdOsoba { get; set; }
    public List<string> Imiona { get; set; } = new();
    public string Nazwisko { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public string NrTelefonu { get; set; } = null!;
    public string Adres { get; set; } = null!;
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    public string Grafik { get; set; } = null!;
}