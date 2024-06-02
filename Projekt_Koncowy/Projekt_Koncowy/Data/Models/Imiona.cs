namespace Projekt_Koncowy.Data.Models;

public class Imiona
{
    public int IdImiona { get; set; }
    public string PierwszeImie { get; set; } = null!;
    public string? DrugieImie { get; set; }

    public virtual ICollection<Osoba> Osoby { get; set; } = null!;
}