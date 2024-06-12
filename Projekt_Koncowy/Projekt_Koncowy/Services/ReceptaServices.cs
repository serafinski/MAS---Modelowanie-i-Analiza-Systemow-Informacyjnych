using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data.DTOs;

namespace Projekt_Koncowy.Services;

public interface IReceptaServices
{
    Task<IEnumerable<ReceptaWyswietlDto>> WyswietlRecepty(int idPacjent);
    Task<ReceptaResponseDto> DodajRecepte(int idWizyta, List<LekNaRecepcieDodajDto> lekiNaRecepcie);
    Task<bool> UsunRecepte(int idRecepta);
}

public class ReceptaServices : IReceptaServices
{
    private readonly MyDbContext _context;

    public ReceptaServices(MyDbContext context)
    {
        _context = context;
    }
        
    private static object GetPacjentDto(Pacjent pacjent)
    {
        if (pacjent is Dorosly dorosly)
        {
            return new DoroslyDto
            {
                Imie = dorosly.Imiona.PierwszeImie,
                Nazwisko = dorosly.Nazwisko,
                Pesel = dorosly.Pesel,
                NrKontaktuAlarmowego = dorosly.NrKontaktuAlarmowego,
                NipPracodawcy = dorosly.NipPracodawcy
            };
        }

        if (pacjent is Dziecko dziecko)
        {
            return new DzieckoDto
            {
                Imie = dziecko.Imiona.PierwszeImie,
                Nazwisko = dziecko.Nazwisko,
                Pesel = dziecko.Pesel,
                NrKontaktuAlarmowego = dziecko.NrKontaktuAlarmowego,
                NazwaSzkoly = dziecko.NazwaSzkoly
            };
        }

        if (pacjent is Senior senior)
        {
            return new SeniorDto
            {
                Imie = senior.Imiona.PierwszeImie,
                Nazwisko = senior.Nazwisko,
                Pesel = senior.Pesel,
                NrKontaktuAlarmowego = senior.NrKontaktuAlarmowego,
                RokPrzejsciaNaEmeryture = senior.RokPrzejsciaNaEmeryture
            };
        }

        throw new ArgumentException("Unknown patient type");
    }
        
    public async Task<IEnumerable<ReceptaWyswietlDto>> WyswietlRecepty(int idPacjent)
    {
        var wizyty = await _context.Wizyty
            .Where(w => w.IdPacjent == idPacjent)
            .Include(w => w.Recepty)
            .ThenInclude(r => r.LekiNaRecepcie)
            .ThenInclude(lnr => lnr.Lek)
            .Include(w => w.Doktor)
            .ThenInclude(d => d.Imiona)
            .Include(w => w.Placowka)
            .ToListAsync();

        var recepty = wizyty.SelectMany(w => w.Recepty);

        return recepty.Select(r => new ReceptaWyswietlDto
        {
            IdRecepta = r.IdRecepta,
            DataWystawienia = r.DataWystawienia,
            Wizyta = new WizytaHistoriaDodajDto
            {
                IdWizyty = r.Wizyta.IdWizyty,
                DataWizyty = r.Wizyta.DataWizyty,
                OpisWizyty = r.Wizyta.OpisWizyty,
                Doktor = new DoktorDto
                {
                    IdDoktor = r.Wizyta.Doktor?.IdDoktor ?? 0,
                    Imie = r.Wizyta.Doktor?.Imiona?.PierwszeImie ?? string.Empty,
                    Nazwisko = r.Wizyta.Doktor?.Nazwisko ?? string.Empty,
                    NrPrawaWykonywaniaZawodu = r.Wizyta.Doktor?.NrPrawaWykonywaniaZawodu ?? string.Empty
                },
                Placowka = new PlacowkaDto
                {
                    IdPlacowka = r.Wizyta.Placowka?.IdPlacowka ?? 0,
                    Nazwa = r.Wizyta.Placowka?.Nazwa ?? string.Empty
                }
            },
            LekiNaRecepcie = r.LekiNaRecepcie
                //OGRANICZENIE ORDERED
                .OrderBy(lnr => lnr.Lek?.NazwaLeku)
                .Select(lnr => new LekNaRecepcieWyswietlDto
                {
                    NazwaLeku = lnr.Lek?.NazwaLeku ?? string.Empty,
                    Ilosc = lnr.Ilosc,
                    Dawkowanie = lnr.Dawkowanie
                }).ToList()
        }).ToList();
    }

    public async Task<ReceptaResponseDto> DodajRecepte(int idWizyta, List<LekNaRecepcieDodajDto> lekiNaRecepcie)
    {
        // Pobierz wizytę
        var wizyta = await _context.Wizyty
            .Include(w => w.Recepty)
            .ThenInclude(r => r.LekiNaRecepcie)
            .ThenInclude(lnr => lnr.Lek)
            .FirstOrDefaultAsync(w => w.IdWizyty == idWizyta);

        if (wizyta == null)
        {
            throw new Exception("Wizyta nie istnieje.");
        }
        
        //OGRANICZENIE XOR
        // Sprawdź interakcje pomiędzy nowymi lekami
        for (int i = 0; i < lekiNaRecepcie.Count; i++)
        {
            for (int j = i + 1; j < lekiNaRecepcie.Count; j++)
            {
                var lek1Id = lekiNaRecepcie[i].IdLek;
                var lek2Id = lekiNaRecepcie[j].IdLek;

                //Czy istnieje interakcja w tabeli interakcji?
                var interactionExists = await _context.InterakcjeLekow.AnyAsync(i =>
                    (i.IdLek1 == lek1Id && i.IdLek2 == lek2Id) ||
                    (i.IdLek1 == lek2Id && i.IdLek2 == lek1Id));

                if (interactionExists)
                {
                    throw new Exception($"Lek {lek1Id} wchodzi w interakcje z lekiem {lek2Id}.");
                }
            }
        }

        // Tworzenie nowej recepty
        var recepta = new Recepta
        {
            DataWystawienia = DateTime.Now,
            IdWizyta = idWizyta
        };

        _context.Recepty.Add(recepta);
        await _context.SaveChangesAsync();

        foreach (var lekNaRecepcieDto in lekiNaRecepcie)
        {
            var lekNaRecepcie = new LekNaRecepcie
            {
                IdRecepta = recepta.IdRecepta,
                IdLek = lekNaRecepcieDto.IdLek,
                Ilosc = lekNaRecepcieDto.Ilosc,
                Dawkowanie = lekNaRecepcieDto.Dawkowanie
            };
            _context.LekiNaRecepcie.Add(lekNaRecepcie);
        }

        await _context.SaveChangesAsync();

        var dodanaRecepta = await _context.Recepty
            .Include(r => r.LekiNaRecepcie)
            .ThenInclude(lnr => lnr.Lek)
            .Include(r => r.Wizyta)
            .ThenInclude(w => w.Doktor)
            .ThenInclude(d => d.Imiona)
            .Include(r => r.Wizyta)
            .ThenInclude(w => w.Pacjent)
            .ThenInclude(p => p.Imiona)
            .FirstOrDefaultAsync(r => r.IdRecepta == recepta.IdRecepta);

        var pacjent = dodanaRecepta.Wizyta.Pacjent;
        var pacjentDto = GetPacjentDto(pacjent);
        var doktor = dodanaRecepta.Wizyta.Doktor;
        var doktorDto = new DoktorDto
        {
            IdDoktor = doktor.IdDoktor,
            Imie = doktor.Imiona.PierwszeImie,
            Nazwisko = doktor.Nazwisko,
            NrPrawaWykonywaniaZawodu = doktor.NrPrawaWykonywaniaZawodu
        };

        return new ReceptaResponseDto
        {
            IdRecepta = dodanaRecepta.IdRecepta,
            DataWystawienia = dodanaRecepta.DataWystawienia,
            IdPacjent = pacjent.IdPacjent,
            Pacjent = pacjentDto,
            Doktor = doktorDto,
            LekiNaRecepcie = dodanaRecepta.LekiNaRecepcie.Select(lnr => new LekNaRecepcieWyswietlDto
            {
                NazwaLeku = lnr.Lek?.NazwaLeku ?? string.Empty,
                Ilosc = lnr.Ilosc,
                Dawkowanie = lnr.Dawkowanie
            }).ToList()
        };
    }

    
    public async Task<bool> UsunRecepte(int idRecepta)
    {
        var recepta = await _context.Recepty.FindAsync(idRecepta);
        if (recepta == null)
        {
            return false;
        }

        _context.Recepty.Remove(recepta);
        await _context.SaveChangesAsync();
        return true;
    }
}