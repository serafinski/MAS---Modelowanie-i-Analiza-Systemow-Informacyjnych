namespace MP02.Models;

public class Szpital
{
    public int IdSzpitala { get; set; }
    public string NazwaSzpitala { get; set; }
    
    // Lista oddziałów jest zarządzana wyłącznie przez klasę Szpital - kompozycja.
    private List<Oddzial> _oddzialy = new List<Oddzial>();
    
    //Lista współdzielona między szpitalami
    private static List<Szpital> _szpitale = new List<Szpital>();

    //Publiczny interfejs do zwracania prywatnej listy szpitali 
    public IEnumerable<Szpital> SzpitaleEnumerable
    {
        get { return _szpitale.AsEnumerable(); }
    }
    
    public Szpital(int idSzpitala, string nazwaSzpitala)
    {
        IdSzpitala = idSzpitala;
        NazwaSzpitala = nazwaSzpitala;
        //Dodajemy do listy kiedy stworzymy szpital
        _szpitale.Add(this);
    }
    
    //Publiczny interfejs do zwracania prywatnej listy oddziałów
    public IEnumerable<Oddzial> OddzialyEnumerable
    {
        get { return _oddzialy.AsEnumerable(); }
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
    
    // Usuwanie szpitala wraz z usunięciem wszystkich oddziałów
    public static void UsunSzpital(int idSzpitala)
    {
        var szpital = _szpitale.FirstOrDefault(s => s.IdSzpitala == idSzpitala);
        if (szpital != null)
        {
            //Usuwanie wszystkich oddziałów przypisanych do szpitala
            szpital._oddzialy.Clear();
            _szpitale.Remove(szpital);
        }
        else
        {
            throw new ArgumentException("Szpital o podanym ID nie istnieje!");
        }
    }
}