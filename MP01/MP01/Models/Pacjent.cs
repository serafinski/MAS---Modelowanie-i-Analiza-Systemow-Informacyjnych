// strona 28

using System.Text.Json;

namespace MP01.Models;

public class Pacjent
{
    // Ekstensja
    public static List<Pacjent> Pacjenci { get; set; }= new();
    
    public Pacjent()
    {
        Id = Pacjenci.Count;
        Pacjenci.Add(this);
        
    }
    
    public int Id { get; set; }
    
    public required string Pesel { get; set; }
    
    // Atr. powtarzalny
    // Posiada jedną lub więcej wartości
    public required List<string> Imie { get; set; }
    
    public required string Nazwisko { get; set; }
    
    // Atr. klasowy
    // Ma tę samą wartość dla wszystkich obiektów danej klasy.
    public static int MinimalnyWiekBezZgodyOpiekuna { get; } = 18;
    
    public required string Telefon { get; set; }
    
    // Atr. opcjonalny
    // Atrybut może nie mieć wartości.
    public string? Email { get; set; }
    
    // Atr. złożony
    // Składa się z atrybutów prostych lub innych atrybutów złożonych.
    public required Adres Adres { get; set; }
    
    // Przesłonięcie
    public override string ToString()
    {
        var imiona = string.Join(" ", Imie);
        
        var mieszkanie = "";

        if (Adres.NumerMieszkania !=  null)
        {
            mieszkanie = "/" + Adres.NumerMieszkania.ToString();
        }

        var adresTekst = Adres.Ulica + " " + Adres.Numer + mieszkanie + ", " + Adres.KodPocztowy + ", " + Adres.Miasto;

        var emailTekst = "";
        if (!string.IsNullOrEmpty(Email))
        {
            emailTekst = "Email: " + Email;
        }

        return $"Pajent: {imiona} {Nazwisko} {emailTekst}, Adres: {adresTekst}";
    }
    
    // Met. klasowa - znajdź pacjenta po peselu
    public static string ListaPacjentow()
    {
        return string.Join(", ", Pacjenci);
    }
    
    // Przeciążenie
    public void Kaszlnij()
    {
        Console.WriteLine($"Pacjent {Imie[0]} {Nazwisko} kaszle.");
    }

    public void Kaszlnij(int x)
    {
        Console.WriteLine("Atak kaszlu!");
        for (int i = 0; i < x; i++)
        {
            Kaszlnij();
        }
    }
    
    // Ekstensja - trwałość
    public static void LoadJson(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }
        
        var data = File.ReadAllText(path);
        var json = JsonSerializer.Deserialize <List<Pacjent>>(data);
        Pacjenci = json!;
    }
    
    // Ekstensja - trwałość
    public static void SaveJson(string path)
    {
        var data = JsonSerializer.Serialize(Pacjenci,new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        File.WriteAllText(path, data);
    }
}

