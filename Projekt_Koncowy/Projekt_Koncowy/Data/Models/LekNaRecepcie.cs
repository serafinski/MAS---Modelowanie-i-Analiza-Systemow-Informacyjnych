namespace Projekt_Koncowy.Data.Models;

// ASOCJACJA Z ATRYBUTEM
public class LekNaRecepcie
{
    public string Ilosc { get; set; } = null!;

    public string Dawkowanie { get; set; } = null!;
    
    //Połączenie z Lek
    public int IdLek { get; set; } 
        
    //Połączenie z Recepta
    public int IdRecepta { get; set; }
    
    //Virtuals
    public virtual Lek Lek { get; set; } = null!;
    public virtual Recepta Recepta { get; set; } = null!;
}