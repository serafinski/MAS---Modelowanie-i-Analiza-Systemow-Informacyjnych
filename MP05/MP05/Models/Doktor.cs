namespace MP05.Models;

// Klasa, by pokazać: MR — dziedziczenie
// Klasa, by pokazać: MR — asocjacje (1: *)
public class Doktor : Osoba
{
    public int IdDoktor { get; set; }
    public string NumerPrawaWykonywaniaZawodu { get; set; } = null!;
    
    //Lista wizyt
    //Jeden doktor może mieć wiele wizyt (1: *)
    public virtual ICollection<Wizyta> Wizyty { get; set; } = null!;
    
}