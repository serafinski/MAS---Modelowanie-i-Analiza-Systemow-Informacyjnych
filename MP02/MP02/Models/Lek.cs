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
}