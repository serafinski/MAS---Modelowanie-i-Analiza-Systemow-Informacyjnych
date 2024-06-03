namespace Projekt_Koncowy.Data.Models;

public abstract class Pacjent : Osoba
{
    public int IdPacjent
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NrKontaktuAlarmowego { get; set; } = null!;
    
    //Połączenie do Wizyty
    public virtual ICollection<Wizyta> Wizyty { get; set; } = null!;
    
    public override string WyswietlDane()
    {
        return $"Pacjent:\nID: {IdPacjent}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}";
    }
}