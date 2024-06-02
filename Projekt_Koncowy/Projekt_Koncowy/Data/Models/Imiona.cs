namespace Projekt_Koncowy.Data.Models;

public class Imiona
{
    public int IdImion { get; set; }
    public string PierwszeImie { get; set; } = null!;
    public string? DrugieImie { get; set; }

    public virtual ICollection<Osoba> Osoby { get; set; } = null!;
}