namespace Projekt_Koncowy.Data.Models;

// DZIEDZICZENIE WIELOASPEKTOWE
public class Senior : Pacjent
{
    public int IdSenior
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public int RokPrzejsciaNaEmeryture { get; set; }
    
    // PRZESŁONIĘCIE
    public override string WyswietlDane()
    {
        return $"Pacjent:\nID: {IdPacjent}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNr Telefonu: {NrTelefonu}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nCzy jest osobą nieletnią: {CzyNieletnia}" +
               $"\nKategoria wiekowa : {JakaKategoria()}" +
               $"\nNumer kontaktu alarmowego: {NrKontaktuAlarmowego}" +
               $"\nRok przejscia na emeryture: {RokPrzejsciaNaEmeryture}";
    }
}