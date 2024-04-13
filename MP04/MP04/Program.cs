using MP04.Models;

namespace MP04;

class Program
{
    private static void Main(string[] args)
    {
        //OGRANICZENIA
        
        //Ograniczenie Atrybutu
        //Reguła określająca dopuszczalne operacje lub wartości dla atrybutu klasy,
        //mająca na celu zapewnienie poprawności i spójności danych w systemie.
        
        //Dzielenie na klasy pacjenta w zależności od PESELU
        Console.WriteLine("OGRANICZENIE ATRYBUTU\n");
        
        var dziecko = new Pacjent(1, "Jan", "Kowalski","123456789","1524187160");
        var dorosly = new Pacjent(2, "Stefan", "Kowalski","987654321","9404218019");
        var senior = new Pacjent(3, "Piotr", "Kowalski","132546879","5405018961");
        
        dziecko.WyswietlDane();
        dziecko.KategoriaWiekowa.UmowWizyte();
        Console.WriteLine();
        dorosly.WyswietlDane();
        dorosly.KategoriaWiekowa.UmowWizyte();
        Console.WriteLine();
        senior.WyswietlDane();
        senior.KategoriaWiekowa.UmowWizyte();

        //Ograniczenie Unique
        //Atrybut ma unikalną wartość w ramach ekstensji — może istnieć tylko jeden obiekt należący do danej klasy,
        //który ma atrybut z daną wartością
        
        //Nie może być ten sam PESEL
        Console.WriteLine("\nOGRANICZENIE UNIQUE\n");
        try
        {
            //Ten sam pesel
            var dziecko2 = new Pacjent(4, "Jan", "Kowalski", "123456789", "1524187160");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
        }
        
        //Ograniczenie Subset — może być nakładane na dwie asocjacje (lub agregacje)
        //Obydwie asocjacje powinny być pomiędzy tymi samymi klasami oraz obydwa powiązania powinny być pomiędzy tymi samymi obiektami
        
        Console.WriteLine("\nOGRANICZENIE SUBSET\n");
        var pielegniarka = new Pielegniarka("Anna", "Nowak", "7776665555", "9404218011", 1, "98765432");
        pielegniarka.WyswietlDane();
        pielegniarka.PobierzKrew();
        
        //Pielęgniarka Koordynator musi być w również pielęgniarką
        var koordynator = new PielegniarkaKoordynator(1, pielegniarka);
        koordynator.PlanujGrafik();
        
        //Ograniczenie Ordered — może być nakładane na asocjacje lub na klasę
        //Asocjacja — powiązania są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności.
        //Klasa — obiekty w ekstensji są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności
        
        Console.WriteLine("\nOGRANICZENIE ORDERED\n");
        
        var doktor = new Doktor("Andrzej", "Wodecki", "111 222 333",
            "7603030321",1,"7654321");
        var pacjent = new Pacjent(4, "Mateusz", "Nowak", "987 654 321", "012101678901");

        var wizyta1 = new Wizyta(1, DateTime.Now, "Kontrola", doktor, pacjent);
        var wizyta2 = new Wizyta(2, DateTime.Now.AddHours(1),"Diagnoza",doktor,dorosly);
        var wizyta3 = new Wizyta(3, DateTime.Now.AddHours(2), "Szczepienie", doktor, senior);
        doktor.DodajWizyteDoKolejki(wizyta1);
        doktor.DodajWizyteDoKolejki(wizyta2);
        doktor.DodajWizyteDoKolejki(wizyta3);
        Console.WriteLine();
        
        doktor.WyswietlKolejke();


        doktor.ObsluzNastepnaWizyte();
        Console.WriteLine("\nPo obsłudze 1 wizyty:");
        doktor.WyswietlKolejke();
        
        doktor.ObsluzNastepnaWizyte();
        doktor.ObsluzNastepnaWizyte();
        // Brak wizyt
        doktor.ObsluzNastepnaWizyte();
        
        Console.WriteLine("Kolejka po obsłużeniu wizyt:");
        doktor.WyswietlKolejke();
        
        //Ograniczenie Bag — umożliwia przechowywanie duplikatów elementów
        //Asocjacja — może istnieć wiele powiązań pomiędzy tymi samymi obiektami (niedozwolone w klasycznej asocjacji)

        //Ograniczenie Xor — dotyczy co najmniej 2 asocjacji
        //Zapewnia, że będzie istniało tylko jedno powiązanie w ramach asocjacji, które ogranicza.

        //Ograniczenie Własne
        //Niestandardowy warunek/ reguła wprowadzona do modelu systemu, określająca specyficzne wymagania dotyczące jego zachowania lub struktury,
        //nieobjęte przez standardowe elementy UML.
    }
}