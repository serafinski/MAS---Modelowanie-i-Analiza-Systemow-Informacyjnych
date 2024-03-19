using MP01;
using MP01.Models;

// Ekstensja - trwałość
Pacjent.LoadJson("pacjents.json");


var pacjent = new Pacjent
{
    Pesel = "67041662152",
    Imie = new List<string> { "Jan", "Mateusz" },
    Nazwisko = "Kowalski",
    Telefon = "790708165",
    
    // Atr. złożony
    // Składa się z atrybutów prostych lub innych atrybutów złożonych.
    Adres = new Adres()
    {
        Ulica = "Wawerska",
        Numer = "125A",
        NumerMieszkania = 19,
        Miasto = "Warszawa",
        KodPocztowy = "05-123"
    }
};
Console.WriteLine(Pacjent.ListaPacjentow());

// Ekstensja - trwałość
Pacjent.SaveJson("pacjents.json");

pacjent.Kaszlnij();
Console.WriteLine();
pacjent.Kaszlnij(5);