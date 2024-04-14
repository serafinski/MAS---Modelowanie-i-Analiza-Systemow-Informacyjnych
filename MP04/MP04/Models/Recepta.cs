namespace MP04.Models;

public class Recepta
{
    public int IdRecepty { get; set; }
    
    public List<LekiNaRecepcie> LekiNaRecepcie { get; set; }
    
    //Baza interakcji leków — do sprawdzenia XOR'a
    private static readonly Dictionary<int, List<int>> _interakcjeLekow = new Dictionary<int, List<int>>()
    {
        //Id leku — leki, z jakimi wchodzi w interakcje
        {1, [4] },
        {2, [3,4] },
        {3, [2,4] },
        {4, [1,3,2] }
    };
    
    public Recepta(int idRecepty)
    {
        IdRecepty = idRecepty;
        LekiNaRecepcie = new List<LekiNaRecepcie>();
    }
    
    //Funkcja sprawdzająca, czy występują interakcje między lekami
    private bool CzyInstniejeInterakcja(Lek lek1, Lek lek2)
    {
        // Czy lek 1 wchodzi w interakcje z lekiem 2?
        if (_interakcjeLekow.TryGetValue(lek1.IdLeku, out var listaInterakcji))
        {
            if (listaInterakcji.Contains(lek2.IdLeku))
            {
                return true;
            }
        }
        
        // Czy lek 2 wchodzi z interakcje z lekiem 1?
        if (_interakcjeLekow.TryGetValue(lek2.IdLeku, out var listaInterakcji2))
        {
            if (listaInterakcji2.Contains(lek1.IdLeku))
            {
                return true;
            }
        }
        
        //Jeżeli nie ma interakcji 
        return false;
    }
    
    
    //Dodawanie leku do recepty
    public void DodajLekiNaRecepte(Lek lek, int iloscLeku)
    {
        //Sprawdzenie, czy lek od danym Id już nie jest na recepcie
        var lekIstniejeNaRecepcie = LekiNaRecepcie.Any(l => l.Lek.IdLeku == lek.IdLeku);

        if (!lekIstniejeNaRecepcie)
        {
            //Sprawdzanie interakcji między lekami
            foreach (var lnr in LekiNaRecepcie)
            {
                if (CzyInstniejeInterakcja(lek, lnr.Lek))
                {
                    throw new InvalidOperationException($"Nie można dodać leku. {lek.NazwaChemiczna} wchodzi w interakcję z przepisanym lekiem: {lnr.Lek.NazwaChemiczna}.");
                }
            }
            
            var lekNaRecepcie = new LekiNaRecepcie(this, lek, iloscLeku);
            //Śledzenie leków przypisanych do danej recepty
            LekiNaRecepcie.Add(lekNaRecepcie);
            //Śledzenie, na jakich receptach znajduje się danych lek
            lek.LekiNaRecepcie.Add(lekNaRecepcie);
            Console.WriteLine($"IdRecepty:{IdRecepty}, Dodano {lek.NazwaChemiczna} w ilości: {iloscLeku}");
        }
        else
        {
            throw new Exception("Lek o podanym ID już istniej na recepcie!");
        }
    }
}