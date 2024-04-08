namespace MP04;

class Program
{
    private static void Main(string[] args)
    {
        //OGRANICZENIA
        
        //Ograniczenie Atrybutu
        //Reguła określająca dopuszczalne operacje lub wartości dla atrybutu klasy,
        //mająca na celu zapewnienie poprawności i spójności danych w systemie.
        
        //Ograniczenie Unique
        //Atrybut posiada unikalną wartość w ramach ekstensji - może istnieć tylko jeden obiekt należący do danej klasy,
        //który ma atrybut z daną wartością
        
        //Ograniczenie Subset - może być nakładane na dwie asocjacje (lub agregacje)
        //Obydwie asocjacje powinny być pomiędzy tymi samymi klasami oraz obydwa powiązania powinny być pomiędzy tymi samymi obiektami
        
        //Ograniczenie Ordered - może być nakładane na asocjacje lub na klase
        //Asocjacja - powiązania są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności.
        //Klasa - obiekty w ekstensji są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności
        
        //Ograniczenie Bag - umożliwia przechowywanie duplikatów elementów
        //Asocjacja - może istnieć wiele powiązań pomiędzy tymi samymi obiektami (niedozwolone w klasycznej asocjacji)
        
        //Ograniczenie Xor - dotyczy co najmniej 2 asocjacji
        //Zapewnia, że będzie istniało tylko jedno powiązanie w ramach asocjacji, które ogranicza.
        
        //Ograniczenie Własne
        //Niestandardowy warunek/ reguła wprowadzona do modelu systemu, określająca specyficzne wymagania dotyczące jego zachowania lub struktury,
        //nieobjęte przez standardowe elementy UML.
    }
}