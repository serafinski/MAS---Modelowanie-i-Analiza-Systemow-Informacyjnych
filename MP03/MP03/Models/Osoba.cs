namespace MP03.Models;

// Klasa abstrakcyjna i polimorficzne wołanie metod
// Klasa abstrakcyjna - klasa, która nie może mieć bezpośrednich wystąpień (nie mogą istnieć obiekty należące do tej klasy).
public abstract class Osoba
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Telefon { get; set; } 
    public string Pesel { get; set; }
    
    // Kategoria wiekowa - do dziedziczenia wieloaspektowego
    public KategoriaWiekowa KategoriaWiekowa { get; set; }
    
    // Protected - klasa ta ma służyć jako klasa bazowa dla dziedziczenia, a nie do tworzenia bezpośrednich instancji.
    protected Osoba(string imie, string nazwisko, string telefon, string pesel)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        Telefon = telefon;
        Pesel = pesel;
    }
    
    // Dziedziczenie Wieloaspektowe - uwzględnia więcej niż 1 aspekt (kryterium podziału)
    // Dodatkowy konstruktor 
    protected Osoba(string imie, string nazwisko, string telefon, string pesel, KategoriaWiekowa kategoriaWiekowa)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        Telefon = telefon;
        Pesel = pesel;
        KategoriaWiekowa = kategoriaWiekowa;
    }

    // Metoda abstrakcyjna - metoda, która posiada tylko deklarację, ale nie posiada definicji (ciała).
    public abstract void WyswietlDane();
}