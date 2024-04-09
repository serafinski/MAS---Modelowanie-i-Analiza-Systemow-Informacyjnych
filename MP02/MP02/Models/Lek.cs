namespace MP02.Models;

// Asocjacja z atrybutem (klasa asocjacji)
// [] 1-* [] *-1 []
// np. Recepta - LekiNaRecepcie - Lek
public class Lek
{
    public required int IdLeku { get; set; }
    public required string NazwaChemiczna { get; set; }
    
    // Definicja typu i dostępności do listy (zarówno do odczytu, jak i do zapisu) z zewnątrz klasy Lek.
    public List<LekiNaRecepcie> LekiNaRecepcie { get; set; }

    public Lek()
    {
        // Zapewnienie relacji *-*
        // Każdy lek może być przypisany do wielu recept
        LekiNaRecepcie = new List<LekiNaRecepcie>();
    }
    
    //Przydzielenie leku na receptę
    public void PrzydzielNaRecepte(Recepta recepta, int iloscLeku)
    {
        //Sprawdzenie czy lek od danym Id juz nie jest na recepcie
        var lekIstniejeNaRecepcie = recepta.LekiNaRecepcie.Any(l => l.Lek.IdLeku == IdLeku);

        if (!lekIstniejeNaRecepcie)
        {
            var lekNaRecepcie = new LekiNaRecepcie(recepta, this, iloscLeku);

            // Dodajemy powiązanie do recepty
            recepta.LekiNaRecepcie.Add(lekNaRecepcie);
            // Dodajemy powiązanie zwrotne do tego leku
            LekiNaRecepcie.Add(lekNaRecepcie);
        }
        else
        {
            throw new Exception("Ten lek jest już przypisany do podanej recepty.");
        }
    }
}