namespace Projekt_Koncowy.Data.Models;

// DZIEDZICZENIE WIELOASPEKTOWE
public class Pielegniarka : Osoba
{
    public int IdPielegniarka
    {
        get => IdOsoba; 
        set => IdOsoba = value;
    }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;

    public string Grafik { get; set; } = null!; 
    public override string WyswietlDane()
    {
        return $"Pielegniarka:\nID: {IdPielegniarka}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}";
    }
}