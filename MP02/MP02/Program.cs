using MP02.Models;

// (w każdym przypadku: liczności 1-* lub *-* oraz automatyczne tworzenie połączenia zwrotnego)

// Asocjacja zwykła
// Łączenie klas - które mają jakieś biznesowe zależności
// np. Doktor jest przypisany do Wizyty. Doktor może mieć wiele wizyt - relacja 1-*
var doktor = new Doktor
{
    IdDoktora = 1,
    Imie = "Jan",
    Nazwisko = "Kowalski",
    Telefon = "123 456 789",
    NumerPrawaWykonywaniaZawodu = "1234567"
};

var wizyta1 = new Wizyta
{
    IdWizyty = 1,
    OpisWizyty = "Pacjent jest zdrowy"
};

doktor.DodajWizyte(wizyta1);

// Asocjacja z atrybutem (klasa asocjacji)
// [] 1-* [] *-1 []
// np. Recepta - LekiNaRecepcie - Leki
var leki = new List<Lek>
{
    new Lek
    {
        IdLeku = 1,
        NazwaChemiczna = "Aspiryna"
    },
    new Lek
    {
        IdLeku = 2,
        NazwaChemiczna = "Ibuprofen"
    }
};

var recepty = new List<Recepta>
{
    new Recepta
    {
        IdRecepty = 1,
        IloscOpakowan = 2
    }
};

var lekiNaRecepcie = new List<LekiNaRecepcie>
{
    new LekiNaRecepcie
    {
        Lek = leki.FirstOrDefault(lek => lek.IdLeku ==1),
        Recepta = recepty.FirstOrDefault(recepta => recepta.IdRecepty == 1),
        IloscLeku = 5
    }
};

// Asocjacja kwalifikowana
// Polega na tym, iż dostęp do obiektu docelowego odbywa się na podstawie unikatowego kwalifikatora
// Pacjent - Wizyta, pacjent może mieć wiele wizyt (zakładamy 1 wizytę na dzień)
// Kwalifikator - Data_Wizyty


// Kompozycja
// Relacja całość-część, gdzie część nie może istnieć bez całości
// np. Szpital może istnieć bez oddziału kardiologi ale odział kardiologii nie może istnieć bez szpitala
var szpital = new Szpital(1, "Szpital miejski");
szpital.DodajOddzial(1,"Kardiologia");
szpital.DodajOddzial(2,"Neurologia");
szpital.UsunOddzial(1);