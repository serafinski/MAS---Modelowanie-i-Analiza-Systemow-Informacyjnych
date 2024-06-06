using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface IWizytaServices
{
    Task<PacjentHistoriaResponseDto> WyswietlHistorieWizytAsync(int idPacjent);
    Task<WizytaResponseDto?> WyswietlWizyteAsync(int idWizyty);
    Task<WizytaDto> DodajWizyteAsync(WizytaDodajDto dto);
    Task<bool> UsunWizyteAsync(int idWizyty);
}

public class WizytaServices : IWizytaServices
{
    private readonly MyDbContext _context;

    public WizytaServices(MyDbContext context)
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
        
    private static object GetPacjentWithIdDto(Pacjent pacjent)
    {
        if (pacjent is Dorosly dorosly)
        {
            return new DoroslyWizytaDto
            {
                IdPacjent = dorosly.IdPacjent,
                Imie = dorosly.Imiona.PierwszeImie,
                Nazwisko = dorosly.Nazwisko,
                Pesel = dorosly.Pesel,
                NrKontaktuAlarmowego = dorosly.NrKontaktuAlarmowego,
                NipPracodawcy = dorosly.NipPracodawcy
            };
        }

        if (pacjent is Dziecko dziecko)
        {
            return new DzieckoWizytaDto
            {
                IdPacjent = dziecko.IdPacjent,
                Imie = dziecko.Imiona.PierwszeImie,
                Nazwisko = dziecko.Nazwisko,
                Pesel = dziecko.Pesel,
                NrKontaktuAlarmowego = dziecko.NrKontaktuAlarmowego,
                NazwaSzkoly = dziecko.NazwaSzkoly
            };
        }

        if (pacjent is Senior senior)
        {
            return new SeniorWizytaDto
            {
                IdPacjent = senior.IdPacjent,
                Imie = senior.Imiona.PierwszeImie,
                Nazwisko = senior.Nazwisko,
                Pesel = senior.Pesel,
                NrKontaktuAlarmowego = senior.NrKontaktuAlarmowego,
                RokPrzejsciaNaEmeryture = senior.RokPrzejsciaNaEmeryture
            };
        }

        throw new ArgumentException("Unknown patient type");
    }

        
    public async Task<PacjentHistoriaResponseDto> WyswietlHistorieWizytAsync(int idPacjent)
    {
        var pacjent = await _context.Pacjenci
            .Include(p => p.Imiona)
            .Include(p => p.Adres)
            .FirstOrDefaultAsync(p => p.IdPacjent == idPacjent);

        if (pacjent == null) return null;

        var wizyty = await _context.Wizyty
            .Include(w => w.Doktor).ThenInclude(d => d.Imiona)
            .Include(w => w.Placowka)
            .Where(w => w.IdPacjent == idPacjent)
            .Select(w => new WizytaHistoriaDodajDto
            {
                IdWizyty = w.IdWizyty,
                DataWizyty = w.DataWizyty,
                OpisWizyty = w.OpisWizyty,
                Doktor = new DoktorDto
                {
                    IdDoktor = w.Doktor.IdDoktor,
                    Imie = w.Doktor.Imiona.PierwszeImie,
                    Nazwisko = w.Doktor.Nazwisko,
                    NrPrawaWykonywaniaZawodu = w.Doktor.NrPrawaWykonywaniaZawodu
                },
                Placowka = new PlacowkaDto
                {
                    IdPlacowka = w.Placowka.IdPlacowka,
                    Nazwa = w.Placowka.Nazwa
                }
            }).ToListAsync();

        var pacjentDto = GetPacjentDto(pacjent);

        return new PacjentHistoriaResponseDto
        {
            Pacjent = pacjentDto,
            Wizyty = wizyty
        };
    }

    public async Task<WizytaResponseDto?> WyswietlWizyteAsync(int idWizyty)
    {
        var wizyta = await _context.Wizyty
            .Include(w => w.Doktor).ThenInclude(d => d.Imiona)
            .Include(w => w.Pacjent).ThenInclude(p => p.Imiona)
            .Include(w => w.Pacjent).ThenInclude(p => p.Adres)
            .Include(w => w.Placowka)
            .Where(w => w.IdWizyty == idWizyty)
            .Select(w => new WizytaResponseDto
            {
                DataWizyty = w.DataWizyty,
                OpisWizyty = w.OpisWizyty,
                Doktor = new DoktorDto
                {
                    IdDoktor = w.Doktor.IdDoktor,
                    Imie = w.Doktor.Imiona.PierwszeImie,
                    Nazwisko = w.Doktor.Nazwisko,
                    NrPrawaWykonywaniaZawodu = w.Doktor.NrPrawaWykonywaniaZawodu
                },
                Pacjent = GetPacjentWithIdDto(w.Pacjent),
                Placowka = new PlacowkaDto
                {
                    IdPlacowka = w.Placowka.IdPlacowka,
                    Nazwa = w.Placowka.Nazwa
                }
            }).FirstOrDefaultAsync();

        return wizyta;
    }


    public async Task<WizytaDto> DodajWizyteAsync(WizytaDodajDto dto)
    {
        var wizyta = new Wizyta
        {
            IdPacjent = dto.IdPacjent,
            IdDoktor = dto.IdDoktor,
            OpisWizyty = dto.OpisWizyty,
            DataWizyty = dto.DataWizyty,
            IdPlacowka = dto.IdPlacowka
        };

        _context.Wizyty.Add(wizyta);
        await _context.SaveChangesAsync();

        var savedWizyta = await _context.Wizyty
            .Include(w => w.Doktor).ThenInclude(d => d.Imiona)
            .Include(w => w.Pacjent).ThenInclude(p => p.Imiona)
            .Include(w => w.Pacjent).ThenInclude(p => p.Adres)
            .Include(w => w.Placowka)
            .FirstOrDefaultAsync(w => w.IdWizyty == wizyta.IdWizyty);

        if (savedWizyta == null)
        {
            throw new Exception("Failed to retrieve saved visit details.");
        }

        return new WizytaDto
        {
            IdWizyty = savedWizyta.IdWizyty,
            DataWizyty = savedWizyta.DataWizyty,
            OpisWizyty = savedWizyta.OpisWizyty,
            Doktor = new DoktorDto
            {
                IdDoktor = savedWizyta.Doktor.IdDoktor,
                Imie = savedWizyta.Doktor.Imiona.PierwszeImie,
                Nazwisko = savedWizyta.Doktor.Nazwisko,
                NrPrawaWykonywaniaZawodu = savedWizyta.Doktor.NrPrawaWykonywaniaZawodu
            },
            Pacjent = GetPacjentWithIdDto(savedWizyta.Pacjent),
            Placowka = new PlacowkaDto
            {
                IdPlacowka = savedWizyta.Placowka.IdPlacowka,
                Nazwa = savedWizyta.Placowka.Nazwa
            }
        };
    }

    public async Task<bool> UsunWizyteAsync(int idWizyty)
    {
        var wizyta = await _context.Wizyty.FindAsync(idWizyty);
        if (wizyta == null)
        {
            return false;
        }

        _context.Wizyty.Remove(wizyta);
        await _context.SaveChangesAsync();
        return true;
    }
}