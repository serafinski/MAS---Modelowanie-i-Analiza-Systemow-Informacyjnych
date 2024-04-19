namespace MP04.Models;

public class PielegniarkaKoordynator
{
    //DWIE ASOCJACJIE MIEDZY KLASA PIELEGNIARKA A PIELEGNIARKAKOORDYNATOR
    //JEZELI 1 Z NICH ISTNIEJE TO WTEDY => "DOKTOR KIERUJE SZPITALEM JEZELI PRACUJE W SZPITALU"
    private int IdKoordynatora { get; set; }
    
    public Pielegniarka Pielegniarka;

    private static List<PielegniarkaKoordynator> _koordynatorzy = new List<PielegniarkaKoordynator>();

    public static IReadOnlyList<PielegniarkaKoordynator> WszystkieKoordynatory => _koordynatorzy;
    
    public PielegniarkaKoordynator(int idKoordynatora,Pielegniarka pielegniarka)
    {
        //Pielęgniarka Koordynator musi być w również pielęgniarką
        if (pielegniarka == null || !Pielegniarka.WszystkiePielegniarki.Contains(pielegniarka))
        {
            throw new ArgumentException("Pielęgniarka musi być zdefiniowana!");
        }

        IdKoordynatora = idKoordynatora;
        Pielegniarka = pielegniarka;
        _koordynatorzy.Add(this);
    }
    
    public void PlanujGrafik()
    {
        Console.WriteLine($"\nKoordynator o ID: {IdKoordynatora} planuje grafik!");
    }
}