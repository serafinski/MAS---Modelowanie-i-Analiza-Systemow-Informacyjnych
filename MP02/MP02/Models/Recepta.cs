namespace MP02.Models;

// Asocjacja z atrybutem (klasa asocjacji)
// [] 1-* [] *-1 []
// np. Recepta - LekiNaRecepcie - Lek
public class Recepta
{
    public int IdRecepty { get; set; }
    
    public int IloscOpakowan { get; set; }
    
    // Musi być prywatna!
    //STWORZYĆ ASOCJACJE Z KLASY LEKINARECEPCIE DO LEK I RECEPTA - Z POZIOMU KONSTRUKTORA
    private List<LekiNaRecepcie> _lekiNaRecepcie = new List<LekiNaRecepcie>();

    public Recepta(int idRecepty, int iloscOpakowan)
    {
        IdRecepty = idRecepty;
        IloscOpakowan = iloscOpakowan;
    }
    
    //Dodawanie leku do recepty
    public void DodajLek(Lek lek, int iloscLeku, bool inicjalizacja = true)
    {
        if (!CzyLekJestPrzypisany(lek))
        {
            var lekNaRecepcie = new LekiNaRecepcie(this, lek, iloscLeku);
            _lekiNaRecepcie.Add(lekNaRecepcie);
            
            //Sprawdzamy stan — czy zostało już zainicjowane, by uniknąć zapętlenia
            if (inicjalizacja)
            {
                lek.DodajLekNaRecepte(this, iloscLeku, false);
            }
        }
        else
        {
            throw new Exception("Podany lek istnieje już na recepcie!");
        }
    }

    
    public IEnumerable<LekiNaRecepcie> PobierzLekiNaRecepcie()
    {
        return _lekiNaRecepcie.AsReadOnly();
    }

    private bool CzyLekJestPrzypisany(Lek lek)
    {
        return _lekiNaRecepcie.Any(l => l.Lek == lek);
    }
}