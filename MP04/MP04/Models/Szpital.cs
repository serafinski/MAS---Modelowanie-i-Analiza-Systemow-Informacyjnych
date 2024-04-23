namespace MP04.Models;

public class Szpital
{
    private int IdSzpitala { get; set; }
    public string NazwaSzpitala { get; set; }
    
    // Lista oddziałów jest zarządzana wyłącznie przez klasę Szpital - kompozycja.
    private List<Oddzial> _oddzialy = new List<Oddzial>();
    
    //Lista współdzielona między szpitalami
    private static List<Szpital> _szpitale = new List<Szpital>();
    
    //Publiczny interfejs do zwracania prywatnej listy oddziałów
    public IEnumerable<Oddzial> OddzialyEnumerable
    {
        get { return _oddzialy.AsReadOnly(); }
    }
    
    public Szpital(int idSzpitala, string nazwaSzpitala)
    {
        IdSzpitala = idSzpitala;
        NazwaSzpitala = nazwaSzpitala;
        //Dodajemy do listy kiedy stworzymy szpital
        _szpitale.Add(this);
    }
    
    //Kontrola dodawania oddziałów przez klasę Szpital - Kompozycja
    public void DodajOddzial(Oddzial oddzial)
    {
        //Error handling
        if (_oddzialy.Any(o => o.IdOddzialu == oddzial.IdOddzialu))
        {
            throw new ArgumentException("Oddział o tym ID już istnieje w szpitalu!");
        }
        _oddzialy.Add(oddzial);
    }

    //Kontrola usuwania oddziałów przez klasę Szpital - Kompozycja
    public void UsunOddzial(Oddzial oddzial)
    {
        if (_oddzialy.Contains(oddzial))
        {
            _oddzialy.Remove(oddzial);
        }
        else
        {
            throw new ArgumentException("Taki oddział nie istnieje w tym szpitalu!");
        }
    }
    
    // Usuwanie szpitala wraz z usunięciem wszystkich oddziałów
    public static void UsunSzpital(int idSzpitala)
    {
        var szpital = _szpitale.FirstOrDefault(s => s.IdSzpitala == idSzpitala);
        if (szpital != null)
        {
            // Usunięcie wszystkich oddziałów związanych z szpitalem
            szpital._oddzialy.Clear();
            _szpitale.Remove(szpital);  // Usunięcie szpitala z globalnej listy szpitali
        }
        else
        {
            throw new ArgumentException("Szpital o podanym ID nie istnieje!");
        }
    }
}