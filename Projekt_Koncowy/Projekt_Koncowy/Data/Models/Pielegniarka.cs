namespace Projekt_Koncowy.Data.Models;

public class Pielegniarka : Osoba
{
    public int IdPielegniarki { get; set; }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    
    
    public override string WyswietlDane()
    {
        return $"Pielegniarka:\nID: {IdPielegniarki}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nNumer prawa wykonywania zawodu: {NrPrawaWykonywaniaZawodu}";
    }
}