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
        var lekNaRecepcie = new LekiNaRecepcie
        {
            Recepta = this,
            Lek = lek,
            IloscLeku = iloscLeku
        };
        //Sledzenie lekow przypisanych do danej recepty
        LekiNaRecepcie.Add(lekNaRecepcie);
        //Sledzenie na jakich receptach znajduje sie danych lek
        lek.LekiNaRecepcie.Add(lekNaRecepcie);
    }
}