namespace Projekt_Koncowy.Data.DTOs;

public class DodajPielegniarkaDto
{
    public int IdImion { get; set; }
    public string Nazwisko { get; set; } = null!;
    public string Pesel { get; set; } = null!;

    public string NrTelefonu { get; set; } = null!;
    public int IdAdres { get; set; }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    public string Grafik { get; set; } = null!;
}