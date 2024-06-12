using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface IDoktorServices
{
    Task<DoktorWyswietlDto> DodajDoktora(DoktorDodajDto doktorDodajDto);
    Task<bool> UsunDoktora(int id);
}

public class DoktorServices : IDoktorServices
{
    private readonly MyDbContext _context;

    public DoktorServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<DoktorWyswietlDto> DodajDoktora(DoktorDodajDto doktorDodajDto)
    {
        return await DoktorHelper.DodajDoktora(_context, doktorDodajDto);
    }

    public async Task<bool> UsunDoktora(int id)
    {
        return await DoktorHelper.UsunDoktora(_context, id);
    }
}

public static class DoktorHelper
{
    public static async Task<DoktorWyswietlDto> DodajDoktora(MyDbContext context, DoktorDodajDto doktorDodajDto)
    {
        var imiona = new Imiona
        {
            PierwszeImie = doktorDodajDto.Imie,
            DrugieImie = doktorDodajDto.DrugieImie // Może być null
        };

        context.Imiona.Add(imiona);
        await context.SaveChangesAsync();

        var adres = new Adres
        {
            Ulica = doktorDodajDto.Ulica,
            NrDomu = doktorDodajDto.NrDomu,
            NrMieszkania = doktorDodajDto.NrMieszkania,
            KodPocztowy = doktorDodajDto.KodPocztowy,
            Miejscowosc = doktorDodajDto.Miejscowosc
        };

        context.Adresy.Add(adres);
        await context.SaveChangesAsync();

        var doktor = new Doktor
        {
            NrPrawaWykonywaniaZawodu = doktorDodajDto.NrPrawaWykonywaniaZawodu,
            IdImion = imiona.IdImiona,
            Nazwisko = doktorDodajDto.Nazwisko,
            NrTelefonu = doktorDodajDto.NrTelefonu,
            Pesel = doktorDodajDto.Pesel,
            IdAdres = adres.IdAdres
        };

        context.Doktorzy.Add(doktor);
        await context.SaveChangesAsync();

        doktor.IdDoktor = doktor.IdOsoba;
        context.Doktorzy.Update(doktor);
        await context.SaveChangesAsync();

        return new DoktorWyswietlDto
        {
            IdDoktor = doktor.IdDoktor,
            NrPrawaWykonywaniaZawodu = doktor.NrPrawaWykonywaniaZawodu,
            Imie = imiona.PierwszeImie,
            DrugieImie = imiona.DrugieImie,
            Nazwisko = doktor.Nazwisko,
            NrTelefonu = doktor.NrTelefonu,
            Pesel = doktor.Pesel,
            Ulica = adres.Ulica,
            NrDomu = adres.NrDomu,
            NrMieszkania = adres.NrMieszkania,
            KodPocztowy = adres.KodPocztowy,
            Miejscowosc = adres.Miejscowosc
        };
    }

    public static async Task<bool> UsunDoktora(MyDbContext context, int id)
    {
        var doktor = await context.Doktorzy.FindAsync(id);
        if (doktor == null)
        {
            return false;
        }

        var imiona = await context.Imiona.FindAsync(doktor.IdImion);
        var adres = await context.Adresy.FindAsync(doktor.IdAdres);

        context.Doktorzy.Remove(doktor);
        if (imiona != null)
        {
            context.Imiona.Remove(imiona);
        }
        if (adres != null)
        {
            context.Adresy.Remove(adres);
        }
        await context.SaveChangesAsync();
        return true;
    }
}