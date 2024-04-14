namespace MP04.Models;

public class Lek
{
    public int IdLeku { get; set; }
    public string NazwaChemiczna { get; set; }
    
    // Definicja typu i dostępności do listy (zarówno do odczytu, jak i do zapisu) z zewnątrz klasy Lek.
    public List<LekiNaRecepcie> LekiNaRecepcie { get; set; }

    public Lek(int idLeku, string nazwaChemiczna)
    {
        IdLeku = idLeku;
        NazwaChemiczna = nazwaChemiczna;
        LekiNaRecepcie = new List<LekiNaRecepcie>();
    }
    
    //Przydzielenie leku na receptę
    public void PrzydzielNaRecepte(Recepta recepta, int iloscLeku)
    {
        //Sprawdzenie, czy lek od danym Id juz nie jest na recepcie
        var lekIstniejeNaRecepcie = recepta.LekiNaRecepcie.Any(l => l.Lek.IdLeku == IdLeku);

        if (!lekIstniejeNaRecepcie)
        {
            var lekNaRecepcie = new LekiNaRecepcie(recepta, this, iloscLeku);

            // Dodajemy powiązanie do recepty
            recepta.LekiNaRecepcie.Add(lekNaRecepcie);
            // Dodajemy powiązanie zwrotne do tego leku
            LekiNaRecepcie.Add(lekNaRecepcie);
        }
        else
        {
            throw new Exception("Ten lek jest już przypisany do podanej recepty.");
        }
    }
}