using MP02.Models;

namespace MP02;

class Program
{
    private static void Main(string[] args)
    { 
        //ASOCJACJE ZWROTNE - WSZYSTKO MUSI BYĆ PRYWATNE, JEDYNIE METODY TWORZĄCE POWIĄZANIA ZWROTNE MAJĄ BYĆ PUBLICZNE!
        // (w każdym przypadku: liczności 1-* lub *-* oraz automatyczne tworzenie połączenia zwrotnego)

        // Asocjacja zwykła
        // Łączenie klas - które mają jakieś biznesowe zależności
        // np. Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
        var doktor = new Doktor(1, "Jan", "Kowalski","123 456 789",
            "1234567");
        var doktor2 = new Doktor(2, "Andrzej", "Wodecki", "111 222 333",
            "7654321");
        
        var wizyta1 = new Wizyta(1, DateTime.Now, "Pacjent jest zdrowy");
        var wizyta2 = new Wizyta(2, DateTime.Today, "Pacjent jest chory");
        wizyta1.PrzypiszDoktora(doktor);
        wizyta2.PrzypiszDoktora(doktor2);
        
        Console.WriteLine("ASOCJACJA ZWYKŁA\n");
        //Dobijamy się do danych doktora z klasy wizyta - połączenie zwrotne
        Console.WriteLine($"Dane z Wizyta:\nDoktor: {wizyta1.GetDoktor().Imie} {wizyta1.GetDoktor().Nazwisko}");
        //Dobijamy się do danych doktora z klasy doktor
        Console.WriteLine($"Dane z Doktor:\nDoktor: {doktor.Imie} {doktor.Nazwisko}");

        // Asocjacja z atrybutem (klasa asocjacji)
        // [] 1-* [] *-1 []
        // np. Recepta - LekiNaRecepcie - Leki
        var leki = new List<Lek>
        {
            new Lek(1,"Aspriryna"),
            new Lek(2,"Ibuprofen")
        };

        var recepta = new Recepta(1, 2);
        
        
        //Na receptę dodajemy lek
        recepta.DodajLek(leki[0],5);
        
        //Lek jest przydzielany na receptę
        leki[1].DodajLekNaRecepte(recepta,3);
        

        Console.WriteLine("\nASOCJACJA Z ATRYBUTEM\n");
        // Wyświetlenie informacji idąc od Recepty
        Console.WriteLine("Dane z Recepty");
        foreach (var lekNaRecepcie in recepta.PobierzLekiNaRecepcie())
        {
            Console.WriteLine($"IdRecepty: {lekNaRecepcie.Recepta.IdRecepty}, " +
                              $"Lek: {lekNaRecepcie.Lek.NazwaChemiczna}, " +
                              $"Ilość: {lekNaRecepcie.IloscLeku}.");
        }
        //Połączenie zwrotne od - idąc od Leku
        Console.WriteLine("\nDane z Leku");
        foreach (var lek in leki)
        {
            foreach (var lekNaRecepcie in lek.PobierzLekiNaRecepcie())
            {
                Console.WriteLine($"Lek: {lek.NazwaChemiczna}, " +
                                  $"IdRecepty: {lekNaRecepcie.Recepta.IdRecepty}, " +
                                  $"Ilość: {lekNaRecepcie.IloscLeku}.");
            }
        }

        // Asocjacja kwalifikowana
        // Polega na tym, iż dostęp do obiektu docelowego odbywa się na podstawie unikatowego kwalifikatora
        // Pacjent - Wizyta, pacjent może mieć wiele wizyt
        // Kwalifikator - IdWizyty
        Console.WriteLine("\nASOCJACJA KWALIFIKOWANA\n");
        var pacjent1 = new Pacjent(1, "Mateusz", "Nowak", "987 654 321", "010101678901");
        wizyta1.PrzypiszPacjenta(pacjent1);
        wizyta2.PrzypiszPacjenta(pacjent1);

        
        var pobranaWizyta = pacjent1.PobierzWizytePacjenta(1);
        Console.WriteLine($"IDWizyty: {pobranaWizyta.IdWizyty}," +
                          $"\nData wizyty: {pobranaWizyta.DataWizyty}," +
                          $"\nOpis Wizyty: {pobranaWizyta.OpisWizyty}");
        Console.WriteLine();
        var pobranaWizyta2 = pacjent1.PobierzWizytePacjenta(2);
        Console.WriteLine($"IDWizyty: {pobranaWizyta2.IdWizyty}," +
                          $"\nData wizyty: {pobranaWizyta2.DataWizyty}," +
                          $"\nOpis Wizyty: {pobranaWizyta2.OpisWizyty}");
        
        // Kompozycja
        // Relacja całość-część, gdzie część nie może istnieć bez całości
        // np. Szpital może istnieć bez oddziału kardiologi ale odział kardiologii nie może istnieć bez szpitala
        Console.WriteLine("\nKOMPOZYCJA\n");
        var szpital = new Szpital(1, "Szpital miejski");
        var kardiologia = new Oddzial(1, "Kardiologia", szpital);
        var neurologia = new Oddzial(2, "Neurologia", szpital);
        szpital.UsunOddzial(kardiologia);
        
        //Wypisanie listy oddziałów po usunięciu kardiologi, przed usunięciem szpitala
        foreach (var oddzial in szpital.OddzialyEnumerable)
        {
            Console.WriteLine($"Oddział: {oddzial.NazwaOddzialu}, Szpital: {oddzial.GetSzpital().NazwaSzpitala}");
        }
        Szpital.UsunSzpital(1);
        
        // Sprawdzanie, czy lista oddziałów jest pusta po usunięciu szpitala
        if (!szpital.OddzialyEnumerable.Any())
        {
            Console.WriteLine("Brak oddziałów w szpitalu.");
        }
        else
        {
            foreach (var oddzial in szpital.OddzialyEnumerable)
            {
                Console.WriteLine($"Oddział: {oddzial.NazwaOddzialu}, Szpital: {oddzial.GetSzpital().NazwaSzpitala}");
            }
        }
    }
}