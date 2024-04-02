namespace MP02.Models;

public class Wizyta
{
    public required int IdWizyty { get; set; }
    public required string OpisWizyty { get; set; }
    
    // Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
    public Doktor Doktor { get; set; }
}