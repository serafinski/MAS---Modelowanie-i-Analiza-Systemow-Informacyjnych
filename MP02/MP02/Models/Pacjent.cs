namespace MP02.Models;

public class Pacjent
{
    public int IdPacjenta { get; set; }
    public required string Imie { get; set; }
    public required string Nazwisko { get; set; }
    public required string Telefon { get; set; }
    public required string PESEL { get; set; }
    
    //Zapewnienie unikalnego kwalifikatora - id wizyty
    private Dictionary<int, Wizyta> _wizyty = new Dictionary<int, Wizyta>();

    public void DodajWizytePacjenta(Wizyta wizyta)
    {
        if (!_wizyty.ContainsKey(wizyta.IdWizyty))
        {
            _wizyty.Add(wizyta.IdWizyty,wizyta);
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