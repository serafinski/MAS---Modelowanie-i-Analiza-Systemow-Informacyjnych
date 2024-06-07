﻿namespace Projekt_Koncowy.Data.Models;

// Dziedziczenie po doktor - subset?
public class KierownikPlacowki : Doktor
{
    public int IdKierownik
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    
    //Powiązanie z placówką
    public int IdPlacowki { get; set; }
    
    public DateTime DataObjeciaStanowiska { get; set; }
    
    //Virtual
    public virtual Placowka Placowka { get; set; } = null!;

    public override string WyswietlDane()
    {
        return $"Kierownik Placowki:\nID: {IdKierownik}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNr Telefonu: {NrTelefonu}" +
               $"\nPESEL: {Pesel}" +
               $"\nAdres: {Adres.Ulica} {Adres.NrDomu} M:{Adres.NrMieszkania} {Adres.KodPocztowy} {Adres.Miejscowosc}" +
               $"\nCzy jest osobą nieletnią: {CzyNieletnia}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}" +
               $"\nIdPlacowki: {IdPlacowki}" +
               $"\nData objecia stanowiska: {DataObjeciaStanowiska}";
    }
}