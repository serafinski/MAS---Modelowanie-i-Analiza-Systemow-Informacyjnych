using MP02.Models;

namespace MP02;

class Program
{
    private static void Main(string[] args)
    { 
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
        doktor.DodajWizyteDoktora(wizyta1);
        doktor2.DodajWizyteDoktora(wizyta2);
        
        Console.WriteLine("ASOCJACJA ZWYKŁA\n");
        //Dobijamy się do danych doktora z klasy wizyta - połączenie zwrotne
        Console.WriteLine($"Dane z Wizyta:\nDoktor: {wizyta1.Doktor.Imie} {wizyta1.Doktor.Nazwisko}");
        //Dobijamy się do danych doktora z klasy doktor
        Console.WriteLine($"Dane z Doktor:\nDoktor: {doktor.Imie} {doktor.Nazwisko}");

        // Asocjacja z atrybutem (klasa asocjacji)
        // [] 1-* [] *-1 []
        // np. Recepta - LekiNaRecepcie - Leki
        var leki = new List<Lek>
        {
            new Lek
            {
                IdLeku = 1,
                NazwaChemiczna = "Aspiryna"
            },
            new Lek
            {
                IdLeku = 2,
                NazwaChemiczna = "Ibuprofen"
            }
        };

        var recepta = new Recepta
        {
            IdRecepty = 1,
            IloscOpakowan = 2
        };

        recepta.DodajLekiNaRecepte(leki[0],5);
        recepta.DodajLekiNaRecepte(leki[1],3);
        

        Console.WriteLine("\nASOCJACJA Z ATRYBUTEM\n");
        // Wyświetlenie informacji idąc od Recepty
        Console.WriteLine("Dane z Recepty");
        foreach (var lekNaRecepcie in recepta.LekiNaRecepcie)
        {
            Console.WriteLine($"IdRecepty: {lekNaRecepcie.Recepta.IdRecepty}, " +
                              $"Lek: {lekNaRecepcie.Lek.NazwaChemiczna}, " +
                              $"Ilość: {lekNaRecepcie.IloscLeku}.");
        }
        //Połączenie zwrotne od - idąc od Leku
        Console.WriteLine("\nDane z Leku");
        foreach (var lek in leki)
        {
            foreach (var lekNaRecepcie in lek.LekiNaRecepcie)
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
        var pacjent1 = new Pacjent
        {
            IdPacjenta = 1,
            Imie = "Mateusz",
            Nazwisko = "Nowak",
            Telefon = "987 654 321",
            PESEL = "010101678901"
        };
        pacjent1.DodajWizytePacjenta(wizyta1);
        pacjent1.DodajWizytePacjenta(wizyta2);
        
        var pobranaWizyta = pacjent1.PobierzWizytePacjenta(1);
        Console.WriteLine($"IDWizyty: {pobranaWizyta.IdWizyty}," +
                          $"\nData wizyty: {pobranaWizyta.DataWizyty}," +
                          $"\nOpis Wizyty: {pobranaWizyta.OpisWizyty}");

        // Kompozycja
        // Relacja całość-część, gdzie część nie może istnieć bez całości
        // np. Szpital może istnieć bez oddziału kardiologi ale odział kardiologii nie może istnieć bez szpitala
        Console.WriteLine("\nKOMPOZYCJA\n");
        var szpital = new Szpital(1, "Szpital miejski");
        szpital.DodajOddzial(1,"Kardiologia");
        szpital.DodajOddzial(2,"Neurologia");
        szpital.UsunOddzial(1);
        
        foreach (var oddzial in szpital.OddzialyEnumerable)
        {
            Console.WriteLine($"Oddział: {oddzial.NazwaOddzialu}, Szpital: {oddzial.Szpital.NazwaSzpitala}");
        }
    }
}