namespace MP03.Models;

// Dziedzicznie Overlapping - za pomocą grupowania
// Zbliżone do wielodziedziczenia - obiekt może należeć do do kilku klas na raz
// (może być ich dowolną kombinacją - będzie miał ich wszystkie cechy)

// Dyskryminitator
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
    public HashSet<TypUslugi> Typy { get; set; } = new HashSet<TypUslugi>();
    
    //Specyficzne dla Badania
    public string CoJestBadane { get; set; }
    
    //Specyficzne dla Konsultacji
    public string SpecjalizacjaLekarza { get; set; }
    
    //Konstruktor badanie
    public UslugaMedyczna(int idUslugi, string nazwaUslugi, string opisUslugi, CoJestBadane coJestBadane)
    {
        IdUslugi = idUslugi;
        NazwaUslugi = nazwaUslugi;
        OpisUslugi = opisUslugi;
        CoJestBadane = coJestBadane.Wartosc;
        Typy.Add(TypUslugi.Badanie);
    }
    
    // Konstruktor konsultacja
    public UslugaMedyczna(int idUslugi, string nazwaUslugi, string opisUslugi, string specjalizacjaLekarza)
    {
        IdUslugi = idUslugi;
        NazwaUslugi = nazwaUslugi;
        OpisUslugi = opisUslugi;
        SpecjalizacjaLekarza = specjalizacjaLekarza;
        Typy.Add(TypUslugi.Konsultacja);
    }
    
    // Konstruktor konsultacjaZBadaniem
    public UslugaMedyczna(int idUslugi, string nazwaUslugi, string opisUslugi, CoJestBadane coJestBadane, string specjalizacjaLekarza)
    {
        IdUslugi = idUslugi;
        NazwaUslugi = nazwaUslugi;
        OpisUslugi = opisUslugi;
        CoJestBadane = coJestBadane.Wartosc;
        SpecjalizacjaLekarza = specjalizacjaLekarza;
        Typy.Add(TypUslugi.Badanie);
        Typy.Add(TypUslugi.Konsultacja);
    }

    public void JakiTyp()
    {
        foreach (var typ in Typy)
        {
            Console.WriteLine($"Typ usługi: {typ}");
        }
    }
    
}