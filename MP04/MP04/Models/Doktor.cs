namespace MP04.Models;

// ASSOCJACJA - DOKTOR PRACUJE W SZPITALU - MUSI ISTNIEC BY ASSOCJACJA - DOKTOR KIERUJE SZPITALEM WYSTAPILA (POD WARUNKIEM ZE PRACUJE W TYM SZPITALU)
// DOKTOR MA LISTE SZPITALI W KTÓRYCH PRACUJE I MA LISTĘ SZPITALI KTÓRYMI KIERUJE. MOZE KIEROWAC SZPITALEM JESLI JEST REKORD ZE W NIM PRACUJE
// MAMY 2 ASOCJACJE CZYLI 2 LISTY PRACOWNIKOW W KLASIE DOKTOR - SA 2 LISTY SZPITALI JEDNA NAZYWA SIE PRACUJEW A DRUGA KIERUJE
// JAK ZROBIMY METODE ZOSTANKIEROWNIKIEMSZPITALA W KLASIE DOKTOR KTORA PRZYJMUJE SZPITAL - TO MUSIMY SPRAWDZIC CZY TEN DOKTOR PRACUJE W DANYM SZPITALU
public class Doktor : Osoba
{
    private int IdDoktora { get; set; }
    private string NumerPrawaWykonywaniaZawodu { get; set; }

    //Ograniczenie Własne
    private const int MaxLiczbaWizytNaDzien = 10;
    
    //Listy do Subset'a
    private List<Szpital> PracujeWSzpitalu { get; set; } = new List<Szpital>();
    private List<Szpital> KierujeSzpitalem { get; set; } = new List<Szpital>();

    public Doktor(string imie, string nazwisko, string telefon, string pesel, int idDoktora, string numerPrawaWykonywaniaZawodu) : base(imie, nazwisko, telefon, pesel)
    {
        IdDoktora = idDoktora;
        NumerPrawaWykonywaniaZawodu = numerPrawaWykonywaniaZawodu;
    }

    //Gwarancja Ordered — kolejka na zasadzie FIFO
    private Queue<Wizyta> KolejkaWizyt { get; set; } = new Queue<Wizyta>();
    
    //Bag — lista wszystkich wizyt doktora, gdzie doktor może mieć wizytę z tym samym pacjentem po kilka razy
    private List<Wizyta> WszystkieWizyty { get; set; } = new List<Wizyta>();

    private int LiczbaWizytDzisiaj(DateTime data)
    {
        return WszystkieWizyty.Count(wizyta => wizyta.DataWizyty.Date == data.Date);
    }
    
    public void DodajWizyteDoKolejki(Wizyta wizyta)
    {
        //Ograniczenie Własne
        if (LiczbaWizytDzisiaj(wizyta.DataWizyty) < MaxLiczbaWizytNaDzien)
        {
            //Dodawanie wizyt do listy
            WszystkieWizyty.Add(wizyta);
            //Dodawanie wizyt do kolejki
            KolejkaWizyt.Enqueue(wizyta);
            Console.WriteLine($"Dodano wizytę ID: {wizyta.IdWizyty} do kolejki doktora {Imie} {Nazwisko}.");
        }
        else
        {
            Console.WriteLine("Nie można dodać więcej wizyt - osiągnięto max. liczbe wizyt!");
        }
    }
    
    public void ObsluzNastepnaWizyte()
    {
        if (KolejkaWizyt.Count > 0)
        {
            Wizyta wizyta = KolejkaWizyt.Dequeue();
            Console.WriteLine($"Obsługiwano wizytę ID: {wizyta.IdWizyty} dla doktora {Imie} {Nazwisko}.");
        }
        else
        {
            Console.WriteLine("Brak wizyt w kolejce.");
        }
    }
    
    public void WyswietlKolejke()
    {
        Console.WriteLine($"Kolejka wizyt dla doktora {Imie} {Nazwisko}:");
        foreach (var wizyta in KolejkaWizyt)
        {
            Console.WriteLine($"Wizyta ID: {wizyta.IdWizyty}, Data: {wizyta.DataWizyty},\n" +
                              $"Pacjent: {wizyta.Pacjent.Imie} {wizyta.Pacjent.Nazwisko}");
            Console.WriteLine();
        }
    }
    
    //Wyświetlanie wszystkich wizyt pacjenta u danego doktora
    public void WyswietlWizytyPacjenta(int idPacjenta)
    {
        var wizytyPacjenta = WszystkieWizyty.Where(w => w.Pacjent.IdPacjenta == idPacjenta);

        if (wizytyPacjenta.Any())
        {
            Console.WriteLine($"Wizyty pacjenta o ID: {idPacjenta} u doktora: {Imie} {Nazwisko}:");
            foreach (var wizyta in wizytyPacjenta)
            {
                Console.WriteLine($"ID Wizyty: {wizyta.IdWizyty}\n" +
                                  $"Data wizyty: {wizyta.DataWizyty}\n" +
                                  $"Opis wizyt: {wizyta.OpisWizyty}");
                Console.WriteLine();
            }
            
        }
        else
        {
            Console.WriteLine($"Brak wizyt dla pacjenta o ID {idPacjenta}");
        }
    }
    
    //Metody do Subset
    //Metoda do dodawania doktora do pracy w danym szpitalu
    public void DodajDoktoraDoSzpitala(Szpital szpital)
    {
        if (!PracujeWSzpitalu.Contains(szpital))
        {
            PracujeWSzpitalu.Add(szpital);
        }
        else
        {
            Console.WriteLine($"Dr {Imie} {Nazwisko} pracuje juz w tym szpitalu!");
        }
    }
    
    //Metoda pozwalajaca dodać doktora kierującego szpitalem — pod warunkiem że już w nim pracuje
    public void ZostanKierownikiemSzpitala(Szpital szpital)
    {
        //Gwarancja 2 assocjacji
        if (PracujeWSzpitalu.Contains(szpital))
        {
            if (!KierujeSzpitalem.Contains(szpital))
            {
                KierujeSzpitalem.Add(szpital);
                Console.WriteLine($"Dr {Imie} {Nazwisko} został kierownikiem szpitala: {szpital.NazwaSzpitala}");
            }
            else
            {
                Console.WriteLine($"Dr {Imie} {Nazwisko} już jest kierownikiem {szpital.NazwaSzpitala}");
            }
        }
        else
        {
            throw new InvalidOperationException("Doktor nie może kierować szpitalem w którym nie pracuje!");
        }
    }
    
    public override void WyswietlDane()
    {
        Console.WriteLine($"IdDoktora: {IdDoktora}," +
                          $"\nImie: {Imie}," +
                          $"\nNazwisko: {Nazwisko}," +
                          $"\nTelefon: {Telefon}," +
                          $"\nPESEL: {Pesel}," +
                          $"\nNumer prawa wykonywania zawodu: {NumerPrawaWykonywaniaZawodu}");
    }
}