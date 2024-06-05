namespace Projekt_Koncowy.Data.Models;

public enum KategoriaWiekowa
{
    Dziecko,
    Dorosly,
    Senior
}

// KLASA ABSTRAKCYJNA
public abstract class Osoba
{
    public int IdOsoba { get; set; }
    
    //Imiona
    public int IdImion { get; set; }
    
    public string Nazwisko { get; set; } = null!;

    public string NrTelefonu { get; set; } = null!;

    public string Pesel { get; set; } = null!;
    
    //Adres
    public int IdAdres { get; set; }
    
    //Virtuals
    public virtual Imiona Imiona { get; set; } = null!;
    public virtual Adres Adres { get; set; } = null!;
    
    // ATRYBUT KLASOWY
    private const int MinimalnyWiekBezZgodyOpiekuna = 18;

    // ATRYBUT POCHODNY
    public bool CzyNieletnia => ObliczWiek() < MinimalnyWiekBezZgodyOpiekuna;

    private int ObliczWiek()
    {
        var rok = int.Parse(Pesel.Substring(0, 2));
        var miesiac = int.Parse(Pesel.Substring(2, 2));
        var dzien = int.Parse(Pesel.Substring(4, 2));
        
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

        var wiek = dzisiaj.Year - dataUrodzenia.Year;
        
        if (dataUrodzenia > dzisiaj.AddYears(-wiek)) wiek--;

        return wiek;
    }

    public KategoriaWiekowa JakaKategoria()
    {
        var wiek = ObliczWiek();

        if (wiek < 18)
        {
            return KategoriaWiekowa.Dziecko;
        }
        if (wiek < 65)
        {
            return KategoriaWiekowa.Dorosly;
        }

        return KategoriaWiekowa.Senior;
    }

    public abstract string WyswietlDane();
}