namespace Projekt_Koncowy.Data.DTOs;

public class DzieckoWizytaDto
{
    public int IdPacjent { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public string NrKontaktuAlarmowego { get; set; } = null!;
    public string NazwaSzkoly { get; set; } = null!;
}