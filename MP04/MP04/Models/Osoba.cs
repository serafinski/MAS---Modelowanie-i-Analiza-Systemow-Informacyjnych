namespace MP04.Models;

public abstract class Osoba
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Telefon { get; set; } 
    
    //Prywatne pole PESEL
    private string pesel { get; set; }
    
    //"Baza" do przechowania wszystkich PESEL'i 
    private static HashSet<string> _bazaPeseli = new HashSet<string>();
    
    //Publiczne pole do zarządzania wartością PESEL
    public string Pesel
    {
        //Odczytanie wartości PESEL z wewnątrz klasy 
        get {return pesel;}
        private set
        {
            if (_bazaPeseli.Contains(value))
            {
                throw new InvalidOperationException("BŁĄD: Podany numer PESEL juz istnieje!");
            }
            pesel = value;
            _bazaPeseli.Add(value);
        }
    }
    public KategoriaWiekowa KategoriaWiekowa { get; set; }
    
    protected Osoba(string imie, string nazwisko, string telefon, string pesel)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        Telefon = telefon;
        Pesel = pesel;
        KategoriaWiekowa = JakaKategoria();
    }
    
    //Ograniczenie Atrybutu
    //Reguła określająca dopuszczalne operacje lub wartości dla atrybutu klasy,
    //mająca na celu zapewnienie poprawności i spójności danych w systemie
    private KategoriaWiekowa JakaKategoria()
    {
        //Wyciągnięcie danych z PESEL'u
        int rok = int.Parse(Pesel.Substring(0, 2));
        int miesiac = int.Parse(Pesel.Substring(2, 2));
        int dzien = int.Parse(Pesel.Substring(4, 2));
        
        
        if (miesiac > 20)
        {
            rok += 2000;
            miesiac -= 20;
        }
        
        else
        {
            rok += 1900;
        }

        DateTime dataUrodzenia = new DateTime(rok, miesiac, dzien);
        DateTime dzisiaj = DateTime.Today;

        int wiek = dzisiaj.Year - dataUrodzenia.Year;
        
        //Sprawdź, czy już były urodziny w bieżącym roku
        if (dataUrodzenia > dzisiaj.AddYears(-wiek)) wiek--;

        //Faktyczne ograniczenie
        switch (wiek)
        {
            case < 18:
                return new Dziecko();
            case < 65:
                return new Dorosly();
            default:
                return new Senior();
        }
    }
    
    public abstract void WyswietlDane();
}