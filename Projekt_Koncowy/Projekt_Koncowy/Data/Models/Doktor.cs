namespace Projekt_Koncowy.Data.Models;

public class Doktor : Osoba
{
    public int IdDoktor
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    
    //Połączenie do Wizyty
    public virtual ICollection<Wizyta> Wizyty { get; set; } = null!;
    
    //Wiele doktorów, pracuje w wielu placówkach
    public virtual ICollection<Placowka> Placowki { get; set; } = null!;
    
    public override string WyswietlDane()
    {
        return $"Doktor:\nID: {IdDoktor}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}";
    }
}