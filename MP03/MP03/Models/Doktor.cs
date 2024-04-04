namespace MP03.Models;

// Klasa abstrakcyjna i polimorficzne wołanie metod
public class Doktor : Osoba
{
    public int IdDoktora { get; set; }
    public string NumerPrawaWykonywaniaZawodu { get; set; }

    public Doktor(int idDoktora, string imie, string nazwisko, string telefon, string pesel, string numerPrawaWykonywaniaZawodu) : base(imie, nazwisko, telefon, pesel)
    {
        IdDoktora = idDoktora;
        NumerPrawaWykonywaniaZawodu = numerPrawaWykonywaniaZawodu;
    }
    
    // Polimorficzne wołanie metody - umożliwia odpowiednie wybranie metody do wywołania.
    public override void WyswietlDane()
    {
        Console.WriteLine($"IdDoktora: {IdDoktora}," +
                          $"\nImie: {Imie}," +
                          $"\nNazwisko: {Nazwisko}," +
                          $"\nTelefon: {Telefon}," +
                          $"\nPESEL: {Pesel}");
    }
}