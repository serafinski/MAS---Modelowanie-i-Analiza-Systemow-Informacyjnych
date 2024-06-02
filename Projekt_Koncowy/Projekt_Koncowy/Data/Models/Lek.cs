namespace Projekt_Koncowy.Data.Models;

public class Lek
{
    public int IdLeku { get; set; }
    public string NazwaLeku { get; set; } = null!;
    
    //Połączenie z LekNaRecepcie
    public virtual ICollection<LekNaRecepcie> LekiNaRecepcie { get; set; } = null!;
}