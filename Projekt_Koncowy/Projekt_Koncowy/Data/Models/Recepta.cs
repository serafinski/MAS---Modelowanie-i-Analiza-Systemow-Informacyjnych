namespace Projekt_Koncowy.Data.Models;
// ASOCJACJA Z ATRYBUTEM
public class Recepta
{
    public int IdRecepta { get; set; }
    public DateTime DataWystawienia { get; set; }
    
    //Połączenie z Wizyta
    public int IdWizyta { get; set; }
    
    //Virtual Wizyta
    public virtual Wizyta Wizyta { get; set; } = null!;
    
    //Połączenie z LekNaRecepcie
    public virtual ICollection<LekNaRecepcie> LekiNaRecepcie { get; set; } = null!;
}