namespace Projekt_Koncowy.Data.Models;

public class Placowka
{
    public int IdPlacowki { get; set; }
    public string NazwaPlacowki { get; set; } = null!;
    
    //Powiązanie do kierownika
    public int IdKierownika { get; set; }
    
    //Virtual Kierownika
    public virtual KierownikPlacowki Kierownik { get; set; } = null!;

    //Powiązanie do oddzialu
    public virtual ICollection<Oddzial> Oddzialy { get; set; } = null!;
}