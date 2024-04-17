namespace MP02.Models;

public class Pacjent
{
    public int IdPacjenta { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Telefon { get; set; }
    public string Pesel { get; set; }
    
    //Zapewnienie unikalnego kwalifikatora - id wizyty
    private Dictionary<int, Wizyta> _wizyty = new Dictionary<int, Wizyta>();

    public Pacjent(int idPacjenta, string imie, string nazwisko, string telefon, string pesel)
    {
        IdPacjenta = idPacjenta;
        Imie = imie;
        Nazwisko = nazwisko;
        Telefon = telefon;
        Pesel = pesel;
    }

    public void DodajWizytePacjenta(Wizyta wizyta)
    {
        if (!_wizyty.ContainsKey(wizyta.IdWizyty))
        {
            _wizyty.Add(wizyta.IdWizyty,wizyta);
            //Referencja zwrotna do Pacjenta
            wizyta.PrzypiszPacjenta(this);
        }
        else
        {
            throw new ArgumentException("Wizyta o podanym ID już istnieje!");
        }
    }
    
    // Dostęp przez unikatowy kwalifikator
    public Wizyta PobierzWizytePacjenta(int idWizyty)
    {
        if (_wizyty.TryGetValue(idWizyty, out Wizyta wizyta))
        {
            return wizyta;
        }
        else
        {
            throw new KeyNotFoundException($"Nie znaleziono wizyty o ID: {idWizyty}");
        }
    }
}