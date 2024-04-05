namespace MP03.Models;

// Wielodziedziczenie (Dziedziczenie wielokrotne)
// Wielodziedziczenie - dziedziczymy z więcej niż jednej nadklasy (w przeciwieństwie do abstract)
// W przeciwieństwie do Overlapping - dziedziczymy wszystko - nie jest to konkretna kombinacja
public class PielegniarkaKoordynator : Osoba, IPielegniarkaOddzialowa, IPielegniarka
{
    // Pole klasy
    private int IdKoordynatora { get; set; }
    
    // Pole z interfejsów
    public string NumerPrawaWykonywaniaZawodu { get; set; }
    
    //Konstruktor - z klasy abstrakcyjnej Osoba + dodatkowe pola
    public PielegniarkaKoordynator(int idKoordynatora, string imie, string nazwisko, string telefon, string pesel, string numerPrawaWykonywaniaZawodu) : base(imie, nazwisko, telefon, pesel)
    {
        IdKoordynatora = idKoordynatora;
        NumerPrawaWykonywaniaZawodu = numerPrawaWykonywaniaZawodu;
    }
    
    // Napisana metoda abstrakcyjna
    public override void WyswietlDane()
    {
        Console.WriteLine($"IdPielegnarki: {IdKoordynatora}," +
                          $"\nImie: {Imie}," +
                          $"\nNazwisko: {Nazwisko}," +
                          $"\nTelefon: {Telefon}," +
                          $"\nPESEL: {Pesel}");
    }

    // Metoda z interfejsu IPielegniarkaOddziala 
    public void PlanujGrafik()
    {
        Console.WriteLine($"\nKoordynator o ID: {IdKoordynatora} planuje grafik!");
    }
    
    // Metoda z interfejsu IPielegniarka
    public void PobierzKrew()
    {
        Console.WriteLine($"\nKoordynator o ID: {IdKoordynatora} pobiera krew!");
    }
}