using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;

namespace Projekt_Koncowy.Services;

public interface IGuiServices
{
    Task<DoroslyGuiDto> GetDoroslyByPesel(string pesel);
    Task<DzieckoGuiDto> GetDzieckoByPesel(string pesel);
    Task<SeniorGuiDto> GetSeniorByPesel(string pesel);
}

public class GuiServices : IGuiServices
{
    private readonly MyDbContext _context;

    public GuiServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<DoroslyGuiDto> GetDoroslyByPesel(string pesel)
    {
        var dorosly = await _context.Dorosli
            .Include(p => p.Imiona)
            .Include(p => p.Adres)
            .FirstOrDefaultAsync(p => p.Pesel == pesel);

        if (dorosly == null) return null;

        return new DoroslyGuiDto
        {
            IdPacjenta = dorosly.IdDorosly,
            Imie = dorosly.Imiona.PierwszeImie,
            DrugieImie = dorosly.Imiona.DrugieImie,
            Nazwisko = dorosly.Nazwisko,
            NrTelefonu = dorosly.NrTelefonu,
            Pesel = dorosly.Pesel,
            Ulica = dorosly.Adres.Ulica,
            NrDomu = dorosly.Adres.NrDomu,
            NrMieszkania = dorosly.Adres.NrMieszkania,
            KodPocztowy = dorosly.Adres.KodPocztowy,
            Miejscowosc = dorosly.Adres.Miejscowosc,
            NrKontaktuAlarmowego = dorosly.NrKontaktuAlarmowego,
            NipPracodawcy = dorosly.NipPracodawcy,
            Wizyty = await _context.Wizyty
                .Where(w => w.IdPacjent == dorosly.IdDorosly)
                .Include(w => w.Doktor)
                .Select(w => new WizytaHistoriaDodajDto
                {
                    DataWizyty = w.DataWizyty,
                    OpisWizyty = w.OpisWizyty,
                    Doktor = new DoktorDto
                    {
                        Imie = w.Doktor.Imiona.PierwszeImie,
                        Nazwisko = w.Doktor.Nazwisko,
                        NrPrawaWykonywaniaZawodu = w.Doktor.NrPrawaWykonywaniaZawodu
                    }
                })
                .ToListAsync()
        };
    }

    public async Task<DzieckoGuiDto> GetDzieckoByPesel(string pesel)
    {
        var dziecko = await _context.Dzieci
            .Include(p => p.Imiona)
            .Include(p => p.Adres)
            .FirstOrDefaultAsync(p => p.Pesel == pesel);

        if (dziecko == null) return null;

        return new DzieckoGuiDto
        {
            IdPacjenta = dziecko.IdDziecko,
            Imie = dziecko.Imiona.PierwszeImie,
            DrugieImie = dziecko.Imiona.DrugieImie,
            Nazwisko = dziecko.Nazwisko,
            NrTelefonu = dziecko.NrTelefonu,
            Pesel = dziecko.Pesel,
            Ulica = dziecko.Adres.Ulica,
            NrDomu = dziecko.Adres.NrDomu,
            NrMieszkania = dziecko.Adres.NrMieszkania,
            KodPocztowy = dziecko.Adres.KodPocztowy,
            Miejscowosc = dziecko.Adres.Miejscowosc,
            NrKontaktuAlarmowego = dziecko.NrKontaktuAlarmowego,
            NazwaSzkoly = dziecko.NazwaSzkoly,
            Wizyty = await _context.Wizyty
                .Where(w => w.IdPacjent == dziecko.IdDziecko)
                .Include(w => w.Doktor)
                .Select(w => new WizytaHistoriaDodajDto
                {
                    DataWizyty = w.DataWizyty,
                    OpisWizyty = w.OpisWizyty,
                    Doktor = new DoktorDto
                    {
                        Imie = w.Doktor.Imiona.PierwszeImie,
                        Nazwisko = w.Doktor.Nazwisko,
                        NrPrawaWykonywaniaZawodu = w.Doktor.NrPrawaWykonywaniaZawodu
                    }
                })
                .ToListAsync()
        };
    }

    public async Task<SeniorGuiDto> GetSeniorByPesel(string pesel)
    {
        var senior = await _context.Seniorzy
            .Include(p => p.Imiona)
            .Include(p => p.Adres)
            .FirstOrDefaultAsync(p => p.Pesel == pesel);

        if (senior == null) return null;

        return new SeniorGuiDto
        {
            IdPacjenta = senior.IdSenior,
            Imie = senior.Imiona.PierwszeImie,
            DrugieImie = senior.Imiona.DrugieImie,
            Nazwisko = senior.Nazwisko,
            NrTelefonu = senior.NrTelefonu,
            Pesel = senior.Pesel,
            Ulica = senior.Adres.Ulica,
            NrDomu = senior.Adres.NrDomu,
            NrMieszkania = senior.Adres.NrMieszkania,
            KodPocztowy = senior.Adres.KodPocztowy,
            Miejscowosc = senior.Adres.Miejscowosc,
            NrKontaktuAlarmowego = senior.NrKontaktuAlarmowego,
            RokPrzejsciaNaEmeryture = senior.RokPrzejsciaNaEmeryture,
            Wizyty = await _context.Wizyty
                .Where(w => w.IdPacjent == senior.IdSenior)
                .Include(w => w.Doktor)
                .Select(w => new WizytaHistoriaDodajDto
                {
                    DataWizyty = w.DataWizyty,
                    OpisWizyty = w.OpisWizyty,
                    Doktor = new DoktorDto
                    {
                        Imie = w.Doktor.Imiona.PierwszeImie,
                        Nazwisko = w.Doktor.Nazwisko,
                        NrPrawaWykonywaniaZawodu = w.Doktor.NrPrawaWykonywaniaZawodu
                    }
                })
                .ToListAsync()
        };
    }
}