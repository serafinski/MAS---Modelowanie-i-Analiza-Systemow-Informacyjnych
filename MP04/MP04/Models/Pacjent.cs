namespace MP04.Models;

// Klasa abstrakcyjna i polimorficzne wołanie metod
public class Pacjent : Osoba
{
    public int IdPacjenta { get; set; }
    
    
    // Dziedziczenie Wieloaspektowe - uwzględnia więcej niż 1 aspekt (kryterium podziału)
    // Dodatkowy konstruktor 
    public Pacjent(int idPacjenta, string imie, string nazwisko, string telefon, string pesel) : base(imie, nazwisko, telefon, pesel)
    {
        IdPacjenta = idPacjenta;
    }

    // Polimorficzne wołanie metody - wywołanie tej samej metody w różnych klasach dziedziczących, z różnymi implementacjami.
    public override void WyswietlDane()
    {
        Console.WriteLine($"IdPacjenta: {IdPacjenta}," +
                          $"\nImie: {Imie}," +
                          $"\nNazwisko: {Nazwisko}," +
                          $"\nTelefon: {Telefon}," +
                          $"\nPESEL: {Pesel}");
    }
}