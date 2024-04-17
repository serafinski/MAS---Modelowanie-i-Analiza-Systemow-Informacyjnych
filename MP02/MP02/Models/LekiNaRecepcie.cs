namespace MP02.Models;

// Asocjacja z atrybutem (klasa asocjacji)
// [] 1-* [] *-1 []
// np. Recepta - LekiNaRecepcie - Lek
public class LekiNaRecepcie
{

    public Recepta Recepta { get; set; }
    
    public Lek Lek { get; set; }
    
    public int IloscLeku { get; set; }
    
    //Konstruktor by wartości nie były null'em

    public LekiNaRecepcie(Recepta recepta, Lek lek, int iloscLeku)
    {
        Recepta = recepta;
        Lek = lek;
        IloscLeku = iloscLeku;
    }
}