namespace Projekt_Koncowy.Data.Models;

// Dziedziczenie po doktor - subset?
public class KierownikPlacowki : Doktor
{
    public int IdKierownika { get; set; }
    
    //Powiązanie z placówką
    public int IdPlacowki { get; set; }
    
    public DateTime DataObjeciaStanowiska { get; set; }
    
    //Virtual
    public virtual Placowka Placowka { get; set; } = null!;

    public override string WyswietlDane()
    {
        return $"Kierownik Placowki:\nID: {IdKierownika}" +
               $"\nImie: {Imiona.PierwszeImie} {Imiona.DrugieImie}" +
               $"\nNazwisko: {Nazwisko}" +
               $"\nIdPlacowki: {IdPlacowki}" +
               $"\nData objecia stanowiska: {DataObjeciaStanowiska}";
    }
}