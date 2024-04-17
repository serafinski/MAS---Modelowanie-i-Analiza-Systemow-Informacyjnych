namespace MP02.Models;

public class Wizyta
{
    public int IdWizyty { get; set; }
    
    public DateTime DataWizyty { get; set; }
    
    public string OpisWizyty { get; set; }
    
    // Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
    //DOKTOR MA BYĆ PRYWATNY! + SETTER (DO POŁĄCZEŃ ZWROTNYCH)
    private Doktor Doktor { get; set; }
    
    //Referencja zwrotna do pacjenta
    private Pacjent Pacjent { get; set; }

    public Wizyta(int idWizyty, DateTime dataWizyty, string opisWizyty)
    {
        IdWizyty = idWizyty;
        DataWizyty = dataWizyty;
        OpisWizyty = opisWizyty;
    }

    public void PrzypiszDoktora(Doktor nowyDoktor)
    {
        if ( Doktor != nowyDoktor)
        {
            Doktor = nowyDoktor;
            //PUBLICZNA METODA PRZYPISUJĄCA DOKTORA DO WIZYTY - METODA POWINNA WYWOŁAĆ METODĘ Z KLASY DOKTOR
            //BY ZROBIĆ POŁĄCZENIE ZWROTNE - WYWOŁAĆ JĄ Z POZIOMU DOKTOR.CS
            nowyDoktor.DodajWizyteDoktora(this);
        }
    }

    public void PrzypiszPacjenta(Pacjent nowyPacjent)
    {
        //STWÓRZ IF'A BY NIE DOSZŁO DO ZAPENTLENIA - JEŻELI POŁĄCZENIE ISTNIEJE NIC NIE RÓB (WYJDŹ Z IF'A)
        //JAK NIE ISTNIEJE - STWÓRZ -> I TAK W KAŻDYM MIEJSCU GDZIE NIE MA
        if (Pacjent != nowyPacjent)
        {
            Pacjent = nowyPacjent;
            nowyPacjent.DodajWizytePacjenta(this);
        }
    }

    public Doktor GetDoktor()
    {
        return Doktor;
    }
}