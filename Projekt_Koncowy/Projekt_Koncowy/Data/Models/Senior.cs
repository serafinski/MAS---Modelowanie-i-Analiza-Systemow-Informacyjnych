namespace Projekt_Koncowy.Data.Models;

// DZIEDZICZENIE WIELOASPEKTOWE
public class Senior : Pacjent
{
    public int IdSenior
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string RokPrzejsciaNaEmeryture { get; set; } = null!;
    
    //Wyświetlenie danych
    public override string WyswietlDane()
    {
        return $"Pacjent:\nID: {IdPacjent}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nRok przejscia na emeryture: {RokPrzejsciaNaEmeryture}";
    }
}