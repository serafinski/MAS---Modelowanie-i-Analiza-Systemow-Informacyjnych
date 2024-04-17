namespace MP02.Models;

// Asocjacja z atrybutem (klasa asocjacji)
// [] 1-* [] *-1 []
// np. Recepta - LekiNaRecepcie - Lek
public class Lek
{
    public int IdLeku { get; set; }
    public string NazwaChemiczna { get; set; }
    
    // Musi być prywatna!
    //STWORZYĆ ASOCJACJE Z KLASY LEKINARECEPCIE DO LEK I RECEPTA - Z POZIOMU KONSTRUKTORA
    private List<LekiNaRecepcie> _lekiNaRecepcie = new List<LekiNaRecepcie>();


    public Lek(int idLeku, string nazwaChemiczna)
    {
        IdLeku = idLeku;
        NazwaChemiczna = nazwaChemiczna;
    }
    
    //Przydzielenie leku na receptę
    public void DodajLekNaRecepte(Recepta recepta, int iloscLeku, bool inicjalizacja = true)
    {
        if (!CzyReceptaJestPrzypisana(recepta))
        {
            var lekNaRecepcie = new LekiNaRecepcie(recepta, this, iloscLeku);
            _lekiNaRecepcie.Add(lekNaRecepcie);
            
            //Sprawdzamy stan — czy zostało już zainicjowane, by uniknąć zapętlenia
            if (inicjalizacja)
            {
                recepta.DodajLek(this, iloscLeku, false);
            }
        }
        else
        {
            throw new Exception("Ten lek jest już przypisany do podanej recepty.");
        }
    }

    public IEnumerable<LekiNaRecepcie> PobierzLekiNaRecepcie()
    {
        return _lekiNaRecepcie.AsReadOnly();
    }

    private bool CzyReceptaJestPrzypisana(Recepta recepta)
    {
        return _lekiNaRecepcie.Any(l => l.Recepta == recepta);
    }
}