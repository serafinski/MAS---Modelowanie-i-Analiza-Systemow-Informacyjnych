namespace Projekt_Koncowy.Data.Models;

public class Placowka
{
    public int IdPlacowka { get; set; }
    public string Nazwa { get; set; } = null!;
    
    //Virtual Kierownika - placowka moze miec wielu kierownikow
    public virtual ICollection<KierownikPlacowki> Kierownicy { get; set; } = null!;
    
    // Virtual wizyty
    public virtual ICollection<Wizyta> Wizyty { get; set; } = null!;
    
    //Powiązanie do oddzialu
    //public virtual ICollection<Oddzial> Oddzialy { get; set; } = null!;
}