namespace MP03.Models;

// Dziedzicznie Overlapping
// Zbliżone do wielodziedziczenia - obiekt może należeć do do kilku klas na raz
// (może być ich dowolną kombinacją - będzie miał ich wszystkie cechy)
public enum TypUslugi
{
    Badanie,
    Konsultacja
}

public class UslugaMedyczna
{
    //Wspólne dla każdej z usług
    public int IdUslugi { get; set; }
    public string NazwaUslugi { get; set; }
    public string OpisUslugi { get; set; }
    
    //Odpowiednik EnumSet'a z Javy
    public HashSet<TypUslugi> Typy { get; set; }
    
    //Specyficzne dla Badania
    public string CoJestBadane { get; set; }
    
    //Specyficzne dla Konsultacji
    public string SpecjalizacjaLekarza { get; set; }

    public UslugaMedyczna()
    {
        Typy = new HashSet<TypUslugi>();
    }

    public void DodajTypUslugi(TypUslugi typUslugi)
    {
        Typy.Add(typUslugi);
    }

    public void JakiTyp()
    {
        foreach (var typ in Typy)
        {
            Console.WriteLine($"Typ usługi: {typ}");
        }
    }
    
}