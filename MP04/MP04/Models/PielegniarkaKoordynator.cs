namespace MP04.Models;

public class PielegniarkaKoordynator
{
    private int IdKoordynatora { get; set; }
    
    //Readonly — by nie była modyfikowalna
    private readonly Pielegniarka _pielegniarka;
    
    public PielegniarkaKoordynator(int idKoordynatora,Pielegniarka pielegniarka)
    {
        //Pielęgniarka Koordynator musi być w również pielęgniarką
        if (pielegniarka == null)
        {
            throw new ArgumentException("Pielęgniarka musi być zdefiniowana!");
        }

        IdKoordynatora = idKoordynatora;
        _pielegniarka = pielegniarka;
    }

    public void PlanujGrafik()
    {
        Console.WriteLine($"\nKoordynator o ID: {IdKoordynatora} planuje grafik!");
    }
}