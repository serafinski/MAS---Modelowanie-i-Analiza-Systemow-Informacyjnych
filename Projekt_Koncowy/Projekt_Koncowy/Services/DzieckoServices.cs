using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface IDzieckoServies
{
    Task<DzieckoWyswietlDto> DodajDziecko(DzieckoDodajDto dzieckoDodajDto);
    Task<bool> UsunDziecko(int id);

}

public class DzieckoServices : IDzieckoServies
{
    private readonly MyDbContext _context;

    public DzieckoServices(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<DzieckoWyswietlDto> DodajDziecko(DzieckoDodajDto dzieckoDodajDto)
    {
        var imiona = new Imiona
        {
            PierwszeImie = dzieckoDodajDto.Imie,
            DrugieImie = dzieckoDodajDto.DrugieImie
        };

        _context.Imiona.Add(imiona);
        await _context.SaveChangesAsync();

        var adres = new Adres
        {
            Ulica = dzieckoDodajDto.Ulica,
            NrDomu = dzieckoDodajDto.NrDomu,
            NrMieszkania = dzieckoDodajDto.NrMieszkania,
            KodPocztowy = dzieckoDodajDto.KodPocztowy,
            Miejscowosc = dzieckoDodajDto.Miejscowosc
        };

        _context.Adresy.Add(adres);
        await _context.SaveChangesAsync();

        var dziecko = new Dziecko
        {
            NazwaSzkoly = dzieckoDodajDto.NazwaSzkoly,
            NrKontaktuAlarmowego = dzieckoDodajDto.NrKontaktuAlarmowego,
            IdImion = imiona.IdImiona,
            Nazwisko = dzieckoDodajDto.Nazwisko,
            NrTelefonu = dzieckoDodajDto.NrTelefonu,
            Pesel = dzieckoDodajDto.Pesel,
            IdAdres = adres.IdAdres
        };

        _context.Dzieci.Add(dziecko);
        await _context.SaveChangesAsync();

        dziecko.IdDziecko = dziecko.IdOsoba;
        _context.Dzieci.Update(dziecko);
        await _context.SaveChangesAsync();

        return new DzieckoWyswietlDto
        {
            IdPacjenta = dziecko.IdDziecko,
            Imie = imiona.PierwszeImie,
            DrugieImie = imiona.DrugieImie,
            Nazwisko = dziecko.Nazwisko,
            NrKontaktuAlarmowego = dziecko.NrKontaktuAlarmowego,
            NrTelefonu = dziecko.NrTelefonu,
            Pesel = dziecko.Pesel,
            Ulica = adres.Ulica,
            NrDomu = adres.NrDomu,
            NrMieszkania = adres.NrMieszkania,
            KodPocztowy = adres.KodPocztowy,
            Miejscowosc = adres.Miejscowosc,
            NazwaSzkoly = dziecko.NazwaSzkoly
        };
    }
    
    public async Task<bool> UsunDziecko(int id)
    {
        var dziecko = await _context.Dzieci.FindAsync(id);
        if (dziecko == null)
        {
            return false;
        }

        var imiona = await _context.Imiona.FindAsync(dziecko.IdImion);
        var adres = await _context.Adresy.FindAsync(dziecko.IdAdres);

        _context.Dzieci.Remove(dziecko);
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