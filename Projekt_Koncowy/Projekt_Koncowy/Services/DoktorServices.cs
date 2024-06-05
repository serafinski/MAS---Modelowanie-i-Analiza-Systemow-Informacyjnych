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
        var imiona = new Imiona
        {
            PierwszeImie = doktorDodajDto.Imie,
            // Może być null
            DrugieImie = doktorDodajDto.DrugieImie 
        };

        // Dodaj obiekt Imiona do kontekstu i zapisz zmiany, aby uzyskać IdImiona
        _context.Imiona.Add(imiona);
        await _context.SaveChangesAsync();

        var adres = new Adres
        {
            Ulica = doktorDodajDto.Ulica,
            NrDomu = doktorDodajDto.NrDomu,
            NrMieszkania = doktorDodajDto.NrMieszkania,
            KodPocztowy = doktorDodajDto.KodPocztowy,
            Miejscowosc = doktorDodajDto.Miejscowosc
        };

        _context.Adresy.Add(adres);
        await _context.SaveChangesAsync();

        var doktor = new Doktor
        {
            NrPrawaWykonywaniaZawodu = doktorDodajDto.NrPrawaWykonywaniaZawodu,
            // Przypisz IdImiona do właściwości IdImion
            IdImion = imiona.IdImiona, 
            Nazwisko = doktorDodajDto.Nazwisko,
            NrTelefonu = doktorDodajDto.NrTelefonu,
            Pesel = doktorDodajDto.Pesel,
            IdAdres = adres.IdAdres
        };

        _context.Doktorzy.Add(doktor);
        await _context.SaveChangesAsync();

        // Ustawienie IdDoktor po zapisaniu
        doktor.IdDoktor = doktor.IdOsoba;
        _context.Doktorzy.Update(doktor);
        await _context.SaveChangesAsync();

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

    public async Task<bool> UsunDoktora(int id)
    {
        var doktor = await _context.Doktorzy.FindAsync(id);
        if (doktor == null)
        {
            return false;
        }

        var imiona = await _context.Imiona.FindAsync(doktor.IdImion);
        var adres = await _context.Adresy.FindAsync(doktor.IdAdres);

        _context.Doktorzy.Remove(doktor);
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