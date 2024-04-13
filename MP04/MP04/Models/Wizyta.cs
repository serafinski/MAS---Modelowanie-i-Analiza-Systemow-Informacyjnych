namespace MP04.Models;

public class Wizyta
{
    public int IdWizyty { get; set; }
    
    public DateTime DataWizyty { get; set; }
    
    public string OpisWizyty { get; set; }
    
    public Doktor Doktor { get; set; }
    
    //Referencja do pacjenta przypisanego do listy
    public Pacjent Pacjent { get; set; }

    public Wizyta(int idWizyty, DateTime dataWizyty, string opisWizyty, Doktor doktor, Pacjent pacjent)
    {
        IdWizyty = idWizyty;
        DataWizyty = dataWizyty;
        OpisWizyty = opisWizyty;
        Doktor = doktor;
        Pacjent = pacjent;
    }
}