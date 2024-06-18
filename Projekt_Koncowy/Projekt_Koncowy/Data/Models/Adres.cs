namespace Projekt_Koncowy.Data.Models;

// ATRYBUT ZŁOŻONY
public class Adres
{
    public int IdAdres { get; set; }
    public string Ulica { get; set; } = null!;
    public string NrDomu { get; set; } = null!;
    
    // ATRYBUT OPCJONALNY
    public int? NrMieszkania { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string Miejscowosc { get; set; } = null!;

    public virtual ICollection<Osoba> Osoby { get; set; } = null!;
}