namespace Projekt_Koncowy.Data.DTOs;

public class ReceptaWyswietlDto
{
    public int IdRecepta { get; set; }
    public DateTime DataWystawienia { get; set; }
    public WizytaHistoriaDodajDto Wizyta { get; set; } = null!;
    public List<LekNaRecepcieWyswietlDto> LekiNaRecepcie { get; set; } = null!;
}