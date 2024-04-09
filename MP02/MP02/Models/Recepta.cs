namespace MP02.Models;

// Asocjacja z atrybutem (klasa asocjacji)
// [] 1-* [] *-1 []
// np. Recepta - LekiNaRecepcie - Lek
public class Recepta
{
    public required int IdRecepty { get; set; }
    
    public required int IloscOpakowan { get; set; }
    
    // Definicja typu i dostępności do listy (zarówno do odczytu, jak i do zapisu) z zewnątrz klasy Recepta.
    public List<LekiNaRecepcie> LekiNaRecepcie { get; set; }

    public Recepta()
    {
        // Zapewnienie relacji *-*
        // Każda recepta może zawierać wiele leków
        LekiNaRecepcie = new List<LekiNaRecepcie>();
    }
    
    //Dodawanie leku do recepty
    public void DodajLekiNaRecepte(Lek lek, int iloscLeku)
    {
        //Sprawdzenie czy lek od danym Id juz nie jest na recepcie
        var lekIstniejeNaRecepcie = LekiNaRecepcie.Any(l => l.Lek.IdLeku == lek.IdLeku);

        if (!lekIstniejeNaRecepcie)
        {
            var lekNaRecepcie = new LekiNaRecepcie(this, lek, iloscLeku);
            
            //Śledzenie leków przypisanych do danej recepty
            LekiNaRecepcie.Add(lekNaRecepcie);
            //Śledzenie na jakich receptach znajduje sie danych lek
            lek.LekiNaRecepcie.Add(lekNaRecepcie);
        }
        else
        {
            throw new Exception("Lek o podanym ID już istniej na recepcie!");
        }
    }
}