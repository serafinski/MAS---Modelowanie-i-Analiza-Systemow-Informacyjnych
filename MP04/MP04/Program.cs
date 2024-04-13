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
        Console.WriteLine("OGRANICZENIE UNIQUE\n");
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

        //Ograniczenie Ordered — może być nakładane na asocjacje lub na klase
        //Asocjacja — powiązania są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności.
        //Klasa — obiekty w ekstensji są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności

        //Ograniczenie Bag — umożliwia przechowywanie duplikatów elementów
        //Asocjacja — może istnieć wiele powiązań pomiędzy tymi samymi obiektami (niedozwolone w klasycznej asocjacji)

        //Ograniczenie Xor — dotyczy co najmniej 2 asocjacji
        //Zapewnia, że będzie istniało tylko jedno powiązanie w ramach asocjacji, które ogranicza.

        //Ograniczenie Własne
        //Niestandardowy warunek/ reguła wprowadzona do modelu systemu, określająca specyficzne wymagania dotyczące jego zachowania lub struktury,
        //nieobjęte przez standardowe elementy UML.
    }
}