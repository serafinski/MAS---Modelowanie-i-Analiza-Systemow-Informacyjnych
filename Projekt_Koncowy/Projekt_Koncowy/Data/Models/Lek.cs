namespace Projekt_Koncowy.Data.Models;

// ASOCJACJA Z ATRYBUTEM
public class Lek
{
    public int IdLek { get; set; }
    public string NazwaLeku { get; set; } = null!;
    
    //Połączenie z LekNaRecepcie
    public virtual ICollection<LekNaRecepcie> LekiNaRecepcie { get; set; } = null!;
}