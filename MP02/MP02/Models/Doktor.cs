namespace MP02.Models;

public class Doktor
{
    public int IdDoktora { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Telefon { get; set; }
    public string NumerPrawaWykonywaniaZawodu { get; set; }

    public Doktor(int idDoktora, string imie, string nazwisko, string telefon, string numerPrawaWykonywaniaZawodu)
    {
        IdDoktora = idDoktora;
        Imie = imie;
        Nazwisko = nazwisko;
        Telefon = telefon;
        NumerPrawaWykonywaniaZawodu = numerPrawaWykonywaniaZawodu;
    }

    // Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
    public List<Wizyta> Wizyty { get; set; } = new List<Wizyta>();

    public void DodajWizyteDoktora(Wizyta wizyta)
    {
        if (Wizyty.Any(w => w.IdWizyty == wizyta.IdWizyty))
        {
            throw new ArgumentException("Wizyta o podanym ID już istnieje w liście wizyt doktora!");
        }
        
        Wizyty.Add(wizyta);
        // Tworzenie połączenia zwrotnego
        wizyta.Doktor = this;
    }
}