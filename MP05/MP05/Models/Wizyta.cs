namespace MP05.Models;

// Klasa, by pokazać: MR — asocjacje (1: *)
public class Wizyta
{
    public int IdWizyta { get; set; }
    
    public DateTime DataWizyt { get; set; }

    public string OpisWizyt { get; set; } = null!;
    
    //IdDoktor'a do ForeignKey
    public int IdDoktor { get; set; }
    
    //Wiele wizyt może być prowadzonych przez tego samego doktora (*: 1)
    public virtual Doktor Doktor { get; set; } = null!;
    
}