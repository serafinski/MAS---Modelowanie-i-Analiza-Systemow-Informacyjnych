namespace MP04.Models;

public class Recepta
{
    public int IdRecepty { get; set; }
    
    // Musi być prywatna!
    private List<LekiNaRecepcie> _lekiNaRecepcie = new List<LekiNaRecepcie>();
    
    
    public Recepta(int idRecepty)
    {
        IdRecepty = idRecepty;
    }
    
    //Dodawanie leku do recepty
    public void DodajLekDoRecepty(Lek lek, int iloscLeku, bool inicjalizacja = true)
    {
        if(CzyMozeDodacLek(lek))
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
        else
        {
            throw new InvalidOperationException("Nie można dodać leku, ponieważ wchodzi w interakcje z innym lekiem na recepcie.");
        }
    }
    
    public IEnumerable<LekiNaRecepcie> PobierzLekiNaRecepcie()
    {
        return _lekiNaRecepcie.AsReadOnly();
    }

    public bool CzyMozeDodacLek(Lek nowyLek)
    {
        return _lekiNaRecepcie.All(l => !l.Lek.Interakcje.Contains(nowyLek));
    }
    
    private bool CzyLekJestPrzypisany(Lek lek)
    {
        return _lekiNaRecepcie.Any(l => l.Lek == lek);
    }
}