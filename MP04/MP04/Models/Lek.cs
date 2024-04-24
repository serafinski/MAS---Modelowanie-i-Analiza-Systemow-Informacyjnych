namespace MP04.Models;

//REKURENCYJA ASSOCJACJA LEKU Z LEKIEM - JEŻELI NIE WYSTĘPUJE INTERAKCJA PRZYPISZ NA RECEPTE
//TABLICA NIE JEST OK BO NIE JEST ASOCJACJA
public class Lek
{
    public int IdLeku { get; set; }
    public string NazwaChemiczna { get; set; }
    
    // Musi być prywatna!
    private List<LekiNaRecepcie> _lekiNaRecepcie = new List<LekiNaRecepcie>();
    
    public List<Lek> Interakcje { get; set; }

    public Lek(int idLeku, string nazwaChemiczna)
    {
        IdLeku = idLeku;
        NazwaChemiczna = nazwaChemiczna;
        Interakcje = new List<Lek>();
    }
    
    //Przydzielenie leku na receptę
    public void DodajLekNaRecepte(Recepta recepta, int iloscLeku, bool inicjalizacja = true)
    {
        if(recepta.CzyMozeDodacLek(this)){
            if (!CzyReceptaJestPrzypisana(recepta))
            {
                var lekNaRecepcie = new LekiNaRecepcie(recepta, this, iloscLeku);
                _lekiNaRecepcie.Add(lekNaRecepcie);

                //Sprawdzamy stan — czy zostało już zainicjowane, by uniknąć zapętlenia
                if (inicjalizacja)
                {
                    recepta.DodajLekDoRecepty(this, iloscLeku, false);
                }
            }
            else
            {
                throw new Exception("Ten lek jest już przypisany do podanej recepty.");
            }
        }
        else
        {
            throw new InvalidOperationException("Nie można dodać leku, ponieważ wchodzi w interakcje z innym lekiem na recepcie.");
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