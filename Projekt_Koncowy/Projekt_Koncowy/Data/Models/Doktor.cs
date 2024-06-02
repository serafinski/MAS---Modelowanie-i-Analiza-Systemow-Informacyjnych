﻿namespace Projekt_Koncowy.Data.Models;

public class Doktor : Osoba
{
    public int IdDoktora { get; set; }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    
    //Połączenie do Wizyty
    public virtual ICollection<Wizyta> Wizyty { get; set; } = null!;
    
    public override string WyswietlDane()
    {
        return $"Doktor:\nID: {IdDoktora}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}";
    }
}