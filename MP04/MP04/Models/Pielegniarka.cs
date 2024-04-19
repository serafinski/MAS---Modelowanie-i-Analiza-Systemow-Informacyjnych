namespace MP04.Models;

public class Pielegniarka : Osoba
{
    private int IdPielegniarki { get; set; }

    public string NumerPrawaWykonywaniaZawodu { get; set; }

    //Lista przechowująca wszystkie pielęgniarki
    private static List<Pielegniarka> _pielegniarki = new List<Pielegniarka>();
    
    //Publiczny dostęp do listy
    public static IReadOnlyList<Pielegniarka> WszystkiePielegniarki => _pielegniarki;
    
    public Pielegniarka(string imie, string nazwisko, string telefon, string pesel, int idPielegniarki, string numerPrawaWykonywaniaZawodu) : base(imie, nazwisko, telefon, pesel)
    {
        IdPielegniarki = idPielegniarki;
        NumerPrawaWykonywaniaZawodu = numerPrawaWykonywaniaZawodu;
        //Dodawanie pielęgniarki do listy przy tworzeniu obiektu
        _pielegniarki.Add(this); 
    }
    
    public override void WyswietlDane()
    {
        Console.WriteLine($"IdPielegniarki: {IdPielegniarki}," +
                          $"\nImie: {Imie}," +
                          $"\nNazwisko: {Nazwisko}," +
                          $"\nTelefon: {Telefon}," +
                          $"\nPESEL: {Pesel}," +
                          $"\nNumer prawa wykonywania zawodu: {NumerPrawaWykonywaniaZawodu}");
    }
    
    public void PobierzKrew()
    {
        Console.WriteLine($"\nPielegniarka o ID: {IdPielegniarki} pobiera krew!");
    }
}