namespace MP02.Models;

public class Szpital
{
    public int IdSzpitala { get; set; }
    public string NazwaSzpitala { get; set; }
    
    // Lista oddziałów jest zarządzana wyłącznie przez klasę Szpital - kompozycja.
    private List<Oddzial> _oddzialy = new List<Oddzial>();

    public Szpital(int idSzpitala, string nazwaSzpitala)
    {
        IdSzpitala = idSzpitala;
        NazwaSzpitala = nazwaSzpitala;
    }
    
    //Kontrola dodawania oddziałów przez klasę Szpital - Kompozycja
    public void DodajOddzial(int idOddzialu, string nazwaOddzialu)
    {
        //Error handling
        if (_oddzialy.Any(oddzial => oddzial.IdOddzialu == idOddzialu))
        {
            throw new ArgumentException("Podane ID już istnieje!");
        }
        
        // Połączenie zwrotne - Oddział jest świadomy do którego Szpitala należy
        var oddzial = new Oddzial(idOddzialu, nazwaOddzialu, this);
        _oddzialy.Add(oddzial);
    }

    //Kontrola usuwania oddziałów przez klasę Szpital - Kompozycja
    public void UsunOddzial(int idOddzialu)
    {
        var oddzial = _oddzialy.FirstOrDefault(oddzial => oddzial.IdOddzialu == idOddzialu);

        if (oddzial != null)
        {
            _oddzialy.Remove(oddzial);
        }
        //Error handling
        else
        {
            throw new ArgumentException("Oddział o podanym ID nie istnieje!");
        }
    }
}