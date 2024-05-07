namespace MP05.Models.DTOs;

public class AddDoktorDto
{
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string Telefon { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public string NumerPrawaWykonywaniaZawodu { get; set; } = null!;
}