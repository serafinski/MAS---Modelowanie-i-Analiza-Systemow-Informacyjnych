namespace Projekt_Koncowy.Data.Models;

public class InterakcjaLeku
{
    public int IdLek1 { get; set; }
    public int IdLek2 { get; set; }
    public Lek Lek1 { get; set; }
    public Lek Lek2 { get; set; }
}