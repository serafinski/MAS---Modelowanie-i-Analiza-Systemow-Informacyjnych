namespace Projekt_Koncowy.Data.Models;

// DZIEDZICZENIE WIELOASPEKTOWE
public class Doktor : Osoba
{
    public int IdDoktor
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    
    //Połączenie do Wizyty
    //ASOCJACJA ZWYKŁA
    public virtual ICollection<Wizyta> Wizyty { get; set; } = null!;
    
    // PRZESŁONIĘCIE
    public override string WyswietlDane()
    {
        return $"Doktor:\nID: {IdDoktor}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNr Telefonu: {NrTelefonu}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nCzy jest osobą nieletnią?: {CzyNieletnia}" +
               $"\nKategoria wiekowa : {JakaKategoria()}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}";
    }
}