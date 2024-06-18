namespace Projekt_Koncowy.Data.DTOs;

public class LekNaRecepcieDodajDto
{
    public int IdLek { get; set; }
    public string Ilosc { get; set; } = null!;
    public string Dawkowanie { get; set; } = null!;
}