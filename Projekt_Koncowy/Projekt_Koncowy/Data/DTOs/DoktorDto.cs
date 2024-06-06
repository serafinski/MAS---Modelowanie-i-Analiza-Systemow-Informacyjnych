namespace Projekt_Koncowy.Data.DTOs;

public class DoktorDto
{
    public int IdDoktor { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
}