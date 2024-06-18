namespace Projekt_Koncowy.Data.Models;

public class Oddzial
{
    public int IdOddzial { get; set; }
    public string NazwaOddzial { get; set; } = null!;
    
    //Odwołanie do Placówki
    public int IdPlacowki { get; set; }

    //Virtual
    public virtual Placowka Placowka { get; set; } = null!;
}