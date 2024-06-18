namespace Projekt_Koncowy.Data.DTOs;

public class DzieckoGuiDto
{
    public int IdPacjenta { get; set; }
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
    public string NazwaSzkoly { get; set; } = null!;
    public List<WizytaHistoriaDodajDto> Wizyty { get; set; } = new();
}