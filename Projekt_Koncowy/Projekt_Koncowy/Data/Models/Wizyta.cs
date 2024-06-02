namespace Projekt_Koncowy.Data.Models;

public class Wizyta
{
    public int IdWizyty { get; set; }
    public DateTime DataWizyty { get; set; }
    public string OpisWizyty { get; set; } = null!;
    
    //Połączenie z Doktorem
    public int IdDoktor { get; set; }
    
    //Połączenie z Pacjentem
    public int IdPacjent { get; set; }
    
    //Virtuals
    public virtual Doktor Doktor { get; set; } = null!;
    public virtual Pacjent Pacjent { get; set; } = null!;
    
    //Połączenie z Recepta
    public virtual ICollection<Recepta> Recepty { get; set; } = null!;

}