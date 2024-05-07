namespace MP05.Models;

// Klasa, by pokazać: MR — dziedziczenie
public class Osoba
{
    public int IdOsoba { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string Telefon { get; set; } = null!;
    public string Pesel { get; set; } = null!;
}