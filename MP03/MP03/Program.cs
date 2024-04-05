// MP03 - Dziedziczenie

using MP03.Models;

namespace MP03;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("KLASA ABSTRAKCYJNA I POLIMORFICZNE WYWOŁANIE METOD\n");
        
        // Klasa abstrakcyjna i polimorficzne wołanie metod
        // Klasa abstrakcyjna - klasa, która nie może mieć bezpośrednich wystąpień (nie mogą istnieć obiekty należące do tej klasy).
        // Metoda abstrakcyjna - metoda, która posiada tylko deklarację, ale nie posiada definicji (ciała).
        // Polimorficzne wołanie metody - wywołanie tej samej metody w różnych klasach dziedziczących, z różnymi implementacjami.
        var doktor = new Doktor(1, "Jan", "Kowalski", 
            "123 456 789", "71031678901","7654321");
        var pacjent = new Pacjent(1, "Mateusz","Nowak","987654321","010101678901");
        
        doktor.WyswietlDane();
        Console.WriteLine();
        pacjent.WyswietlDane();
        
        Console.WriteLine("\nDZIEDZICZENIE OVERLAPPING");
        
        // Dziedzicznie Overlapping - za pomocą grupowania
        // Zbliżone do wielodziedziczenia - obiekt może należeć do do kilku klas na raz
        // (może być ich dowolną kombinacją - będzie miał ich wszystkie cechy)
        var badanie = new UslugaMedyczna
        {
            IdUslugi = 1,
            NazwaUslugi = "Morfologia Krwi",
            OpisUslugi = "Badanie morfologi krwi",
            CoJestBadane = "TSH"
        };
        badanie.DodajTypUslugi(TypUslugi.Badanie);
        Console.WriteLine($"\nIdUsługi: {badanie.IdUslugi}");
        badanie.JakiTyp();

        var konsultacja = new UslugaMedyczna
        {
            IdUslugi = 2,
            NazwaUslugi = "Konsultacja neurologiczna",
            OpisUslugi = "Konsultacja z neurologiem specjalizującym się w problemach z równowagą.",
            SpecjalizacjaLekarza = "Neurolog"
        };
        konsultacja.DodajTypUslugi(TypUslugi.Konsultacja);
        Console.WriteLine($"\nIdUsługi: {konsultacja.IdUslugi}");
        konsultacja.JakiTyp();

        var konsultacjaZBadaniem = new UslugaMedyczna
        {
            IdUslugi = 3,
            NazwaUslugi = "Konsultacja chirurgiczna",
            OpisUslugi = "Konsultacja z chirurgiem naczyniowym",
            SpecjalizacjaLekarza = "Chirurg",
            CoJestBadane = "Znamiona na plecach"
        };
        konsultacjaZBadaniem.DodajTypUslugi(TypUslugi.Badanie);
        konsultacjaZBadaniem.DodajTypUslugi(TypUslugi.Konsultacja);
        Console.WriteLine($"\nIdUsługi: {konsultacjaZBadaniem.IdUslugi}");
        konsultacjaZBadaniem.JakiTyp();
        
        Console.WriteLine("\nWIELODZIEDZICZENIE (DZIEDZICZENIE WIELOKROTNE)\n");
        
        // Wielodziedziczenie (Dziedziczenie wielokrotne)
        // Wielodziedziczenie - dziedziczymy z więcej niż jednej nadklasy (w przeciwieństwie do abstract)
        // W przeciwieństwie do Overlapping - dziedziczymy wszystko - nie jest to konkretna kombinacja
        var koordynator = new PielegniarkaKoordynator(1,"Halina","Sosnowska",
            "7776665555","71060665434","98765432");
        koordynator.WyswietlDane();
        koordynator.PlanujGrafik();
        koordynator.PobierzKrew();

        // Dziedziczenie Wieloaspektowe
        // Dziedziczenie Wieloaspektowe - uwzględnia więcej niż 1 aspekt (kryterium podziału)
        

        // Dziedziczenie Dynamiczne
        // Zmiana stanu z jednego na drugi - obiekty podklas mogą dowolnie zmieniać swoją przynależność
        
    }
}