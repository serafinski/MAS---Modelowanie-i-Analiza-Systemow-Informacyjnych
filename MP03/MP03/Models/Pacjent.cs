namespace MP03.Models;

// Klasa abstrakcyjna i polimorficzne wołanie metod
public class Pacjent : Osoba
{
    public int IdPacjenta { get; set; }

    public Pacjent(int idPacjenta, string imie, string nazwisko, string telefon, string pesel) : base(imie, nazwisko, telefon, pesel)
    {
        IdPacjenta = idPacjenta;
    }

    // Dziedziczenie Dynamiczne
    // Zmiana stanu z jednego na drugi - obiekty podklas mogą dowolnie zmieniać swoją przynależność
    // W tym wypadku chcemy by doktor (dziedziczy po osoba) zmienił się na pacjenta
    public Pacjent(Osoba osoba, int idPacjenta) : base(osoba.Imie, osoba.Nazwisko, osoba.Telefon, osoba.Pesel)
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