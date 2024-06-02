﻿namespace Projekt_Koncowy.Data.Models;

public class Senior : Pacjent
{
    public int IdSeniora { get; set; }
    public string RokPrzejsciaNaEmeryture { get; set; } = null!;
    
    //Wyświetlenie danych
    public override string WyswietlDane()
    {
        return $"Pacjent:\nID: {IdPacjenta}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nRok przejscia na emeryture: {RokPrzejsciaNaEmeryture}";
    }
}