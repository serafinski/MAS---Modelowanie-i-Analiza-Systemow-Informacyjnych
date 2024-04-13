namespace MP04.Models;

public class Doktor : Osoba
{
    public int IdDoktora { get; set; }
    public string NumerPrawaWykonywaniaZawodu { get; set; }

    public Doktor(string imie, string nazwisko, string telefon, string pesel, int idDoktora, string numerPrawaWykonywaniaZawodu) : base(imie, nazwisko, telefon, pesel)
    {
        IdDoktora = idDoktora;
        NumerPrawaWykonywaniaZawodu = numerPrawaWykonywaniaZawodu;
    }

    //Gwarancja Ordered — kolejka na zasadzie FIFO
    private Queue<Wizyta> KolejkaWizyt { get; set; } = new Queue<Wizyta>();

    public void DodajWizyteDoKolejki(Wizyta wizyta)
    {
        KolejkaWizyt.Enqueue(wizyta);
        Console.WriteLine($"Dodano wizytę ID: {wizyta.IdWizyty} do kolejki doktora {Imie} {Nazwisko}.");
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