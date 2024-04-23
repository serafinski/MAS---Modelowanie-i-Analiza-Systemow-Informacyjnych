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
        //Subset jest przeciwieństwem XOR'a — jedna asocjacja może istnieć, jeżeli istnieje druga
        
        //Np. doktor kieruje szpitalem, pod warunkiem, iż pracuje w tym szpitalu
        Console.WriteLine("\nOGRANICZENIE SUBSET\n");
        var kierownikOddzialu = new Doktor("Jan","Serafiński","987654321","710304678901",2,
            "1234567");
        var szpital = new Szpital(1, "Zielony szpital");
        kierownikOddzialu.DodajDoktoraDoSzpitala(szpital);
        kierownikOddzialu.ZostanKierownikiemSzpitala(szpital);
        
        try
        {
            var falszywyKierownik = new Doktor("Stefan", "Parmezan", "978645312", "690403678901", 3,
                "1468967");
            falszywyKierownik.ZostanKierownikiemSzpitala(szpital);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
        }
        
        //Ograniczenie Ordered — może być nakładane na asocjacje lub na klasę
        //Asocjacja — powiązania są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności.
        //Klasa — obiekty w ekstensji są przechowywane (oraz otrzymywane i przetwarzane) w pewnej ustalonej kolejności
        
        //Np. kolejka pacjentów u doktora
        Console.WriteLine("\nOGRANICZENIE ORDERED\n");
        
        var doktor = new Doktor("Andrzej", "Wodecki", "111222333",
            "7603030321",1,"7654321");
        var pacjent = new Pacjent(4, "Mateusz", "Nowak", "987654321", "012101678901");

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
        Console.WriteLine("\nOGRANICZENIE BAG\n");
        
        //Np. ten sam pacjent może mieć wiele wizyt u tego samego doktora
        
        //Edit daty by zadziałało dla Ogranicznia Własnego!
        var wizyta4 = new Wizyta(4, DateTime.Now, "Kontrola po miesiącu", doktor, pacjent);
        var wizyta5 = new Wizyta(5, DateTime.Now, "Kontrola po roku", doktor, pacjent);
        doktor.DodajWizyteDoKolejki(wizyta4);
        doktor.DodajWizyteDoKolejki(wizyta5);
        Console.WriteLine();
        doktor.WyswietlWizytyPacjenta(4);
        
        //Ograniczenie Xor — w ramach grupy asocjacji występuje tylko jedno powiązanie naraz
        //Zapewnia, że będzie istniało tylko jedno powiązanie w ramach asocjacji, które ogranicza.
        Console.WriteLine("\nOGRANICZENIE XOR\n");
        
        //Np. dany lek nie może zostać przepisany na receptę, ponieważ będzie wchodził w interakcje z innym lekiem
        var lek1 = new Lek(1, "Paracetamol");
        var lek2 = new Lek(2, "Ibuprofen");
        var lek3 = new Lek(3, "Aspiryna");
        // Definiowanie interakcji
        lek1.Interakcje.Add(lek3);
        // Interakcja dwukierunkowa
        lek3.Interakcje.Add(lek1);  

        var recepta = new Recepta(1);
        recepta.DodajLekDoRecepty(lek1,1);
        recepta.DodajLekDoRecepty(lek2,1);
        Console.WriteLine("Leki na recepcie przed próbą dodania Aspiryny:");
        foreach (var lekNaRecepcie in recepta.PobierzLekiNaRecepcie())
        {
            Console.WriteLine($"Lek: {lekNaRecepcie.Lek.NazwaChemiczna}, Ilość: {lekNaRecepcie.IloscLeku}");
        }
        
        //Dodawanie leku wchodzącego w interakcje
        try
        {
            recepta.DodajLekDoRecepty(lek3,1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
        }


        //Ograniczenie Własne
        //Niestandardowy warunek/ reguła wprowadzona do modelu systemu, określająca specyficzne wymagania dotyczące jego zachowania lub struktury,
        //nieobjęte przez standardowe elementy UML.
        
        //MA TO BYĆ DOWOLNE OGRANICZENIE KTÓRE JEST NIE NA ATRYBUCIE - OGRANICZENIE NA ASSOCJACJI/METODZIE
        // np. doktor może mieć max. 10 wizyt dziennie
        
        Console.WriteLine("\nOGRANICZENIE WŁASNE\n");
        var wizyta6 = new Wizyta(6, DateTime.Now, "Kontrola", doktor, pacjent);
        doktor.DodajWizyteDoKolejki(wizyta6);
        var wizyta7 = new Wizyta(7, DateTime.Now.AddHours(1),"Diagnoza",doktor,dorosly);
        doktor.DodajWizyteDoKolejki(wizyta7);
        var wizyta8 = new Wizyta(8, DateTime.Now.AddHours(2), "Szczepienie", doktor, senior);
        doktor.DodajWizyteDoKolejki(wizyta8);
        var wizyta9 = new Wizyta(9, DateTime.Now, "Kontrola", doktor, pacjent);
        doktor.DodajWizyteDoKolejki(wizyta9);
        var wizyta10 = new Wizyta(10, DateTime.Now.AddHours(1),"Diagnoza",doktor,dorosly);
        doktor.DodajWizyteDoKolejki(wizyta10);
        var wizyta11 = new Wizyta(11, DateTime.Now.AddHours(2), "Szczepienie", doktor, senior);
        doktor.DodajWizyteDoKolejki(wizyta11);
    }
}