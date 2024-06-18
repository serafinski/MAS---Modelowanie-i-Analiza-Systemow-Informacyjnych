using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface IDoroslyServices
{
    Task<DoroslyWyswietlDto> DodajDorosly(DoroslyDodajDto dto);
    Task<bool> UsunDorosly(int id);
}
public class DoroslyServices : IDoroslyServices
{
    private readonly MyDbContext _context;

    public DoroslyServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<DoroslyWyswietlDto> DodajDorosly(DoroslyDodajDto doroslyDodajDto)
    {
        var imiona = new Imiona
        {
            PierwszeImie = doroslyDodajDto.Imie,
            DrugieImie = doroslyDodajDto.DrugieImie
        };

        _context.Imiona.Add(imiona);
        await _context.SaveChangesAsync();

        var adres = new Adres
        {
            Ulica = doroslyDodajDto.Ulica,
            NrDomu = doroslyDodajDto.NrDomu,
            NrMieszkania = doroslyDodajDto.NrMieszkania,
            KodPocztowy = doroslyDodajDto.KodPocztowy,
            Miejscowosc = doroslyDodajDto.Miejscowosc
        };

        _context.Adresy.Add(adres);
        await _context.SaveChangesAsync();

        var dorosly = new Dorosly
        {
            NipPracodawcy = doroslyDodajDto.NipPracodawcy,
            NrKontaktuAlarmowego = doroslyDodajDto.NrKontaktuAlarmowego,
            IdImion = imiona.IdImiona,
            Nazwisko = doroslyDodajDto.Nazwisko,
            NrTelefonu = doroslyDodajDto.NrTelefonu,
            Pesel = doroslyDodajDto.Pesel,
            IdAdres = adres.IdAdres
        };

        _context.Dorosli.Add(dorosly);
        await _context.SaveChangesAsync();

        dorosly.IdDorosly = dorosly.IdOsoba;
        _context.Dorosli.Update(dorosly);
        await _context.SaveChangesAsync();

        return new DoroslyWyswietlDto
        {
            IdPacjenta = dorosly.IdDorosly,
            Imie = imiona.PierwszeImie,
            DrugieImie = imiona.DrugieImie,
            Nazwisko = dorosly.Nazwisko,
            NrKontaktuAlarmowego = dorosly.NrKontaktuAlarmowego,
            NrTelefonu = dorosly.NrTelefonu,
            Pesel = dorosly.Pesel,
            Ulica = adres.Ulica,
            NrDomu = adres.NrDomu,
            NrMieszkania = adres.NrMieszkania,
            KodPocztowy = adres.KodPocztowy,
            Miejscowosc = adres.Miejscowosc,
            NipPracodawcy = dorosly.NipPracodawcy
        };
    }

    public async Task<bool> UsunDorosly(int id)
    {
        var dorosly = await _context.Dorosli.FindAsync(id);
        if (dorosly == null)
        {
            return false;
        }

        var imiona = await _context.Imiona.FindAsync(dorosly.IdImion);
        var adres = await _context.Adresy.FindAsync(dorosly.IdAdres);

        _context.Dorosli.Remove(dorosly);
        if (imiona != null)
        {
            _context.Imiona.Remove(imiona);
        }
        if (adres != null)
        {
            _context.Adresy.Remove(adres);
        }
        await _context.SaveChangesAsync();
        return true;
    }
}