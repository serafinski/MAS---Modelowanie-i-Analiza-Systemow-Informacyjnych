namespace Projekt_Koncowy.Data.Models;

public class Dorosly : Pacjent
{
    public int IdDorosly
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NipPracodawcy { get; set; } = null!;
    
    //Wyświetlenie danych
    public override string WyswietlDane()
    {
        return $"Pacjent:\nID: {IdPacjent}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nNIP Pracodawcy: {NipPracodawcy}";
    }
}