namespace Projekt_Koncowy.Data.Models;

// DZIEDZICZENIE WIELOASPEKTOWE
public class Pielegniarka : Osoba
{
    public int IdPielegniarka
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;

    public string Grafik { get; set; } = null!; 
    
    // PRZESŁONIĘCIE
    public override string WyswietlDane()
    {
        return $"Pielegniarka:\nID: {IdPielegniarka}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNr Telefonu: {NrTelefonu}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nCzy jest osobą nieletnią?: {CzyNieletnia}" +
               $"\nKategoria wiekowa : {JakaKategoria()}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}" +
               $"\nGrafik: {Grafik}";
    }
}