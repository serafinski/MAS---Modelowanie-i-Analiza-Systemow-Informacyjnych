namespace Projekt_Koncowy.Data.DTOs;

public class SeniorDodajDto
{
    public string Imie { get; set; } = null!;
    public string? DrugieImie { get; set; }
    public string Nazwisko { get; set; } = null!;
    public string NrKontaktuAlarmowego { get; set; } = null!;
    public string NrTelefonu { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public string Ulica { get; set; } = null!;
    public string NrDomu { get; set; } = null!;
    public int? NrMieszkania { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string Miejscowosc { get; set; } = null!;
    //Specyficzne w zależności od rodzaju pacjenta
    public int RokPrzejsciaNaEmeryture { get; set; }
}