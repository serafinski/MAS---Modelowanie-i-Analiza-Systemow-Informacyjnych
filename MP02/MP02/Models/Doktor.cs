namespace MP02.Models;

public class Doktor
{
    public required int IdDoktora { get; set; }
    public required string Imie { get; set; }
    public required string Nazwisko { get; set; }
    public required string Telefon { get; set; }
    public required string NumerPrawaWykonywaniaZawodu { get; set; }

    // Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
    public List<Wizyta> Wizyty { get; set; } = new List<Wizyta>();

    public void DodajWizyte(Wizyta wizyta)
    {
        Wizyty.Add(wizyta);
        // Tworzenie połączenia zwrotnego
        wizyta.Doktor = this;
    }
}