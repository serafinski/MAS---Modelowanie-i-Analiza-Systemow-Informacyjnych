namespace Projekt_Koncowy.Data.Models;

public class Oddzial
{
    public int IdOddzialu { get; set; }
    public string NazwaOddzialu { get; set; } = null!;
    
    //Odwołanie do Placówki
    public int IdPlacowki { get; set; }

    //Virtual
    public virtual Placowka Placowka { get; set; } = null!;
}