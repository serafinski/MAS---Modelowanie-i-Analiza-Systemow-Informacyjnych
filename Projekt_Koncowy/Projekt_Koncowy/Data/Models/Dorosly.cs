﻿namespace Projekt_Koncowy.Data.Models;

// DZIEDZICZENIE WIELOASPEKTOWE
public class Dorosly : Pacjent
{
    public int IdDorosly
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NipPracodawcy { get; set; } = null!;
    
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
               $"\nNIP Pracodawcy: {NipPracodawcy}";
    }
}