namespace MP02.Models;

public class Wizyta
{
    public int IdWizyty { get; set; }
    
    public DateTime DataWizyty { get; set; }
    
    public string OpisWizyty { get; set; }
    
    // Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
    //PRYWATNY + SETTER (DO POŁĄCZEŃ ZWROTNYCH)
    public Doktor Doktor { get; set; }
    
    //Referencja zwrotna do pacjenta
    public Pacjent Pacjent { get; set; }

    public Wizyta(int idWizyty, DateTime dataWizyty, string opisWizyty)
    {
        IdWizyty = idWizyty;
        DataWizyty = dataWizyty;
        OpisWizyty = opisWizyty;
    }
}