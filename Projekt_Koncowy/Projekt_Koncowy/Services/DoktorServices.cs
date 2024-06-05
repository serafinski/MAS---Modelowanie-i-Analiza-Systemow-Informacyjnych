using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface IDoktorServices
{
    Task<WyswietlDoktorDto> DodajDoktora(DodajDoktorDto dodajDoktorDto);
    Task<bool> UsunDoktora(int id);
}

public class DoktorServices : IDoktorServices
{
    private readonly MyDbContext _context;

    public DoktorServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<WyswietlDoktorDto> DodajDoktora(DodajDoktorDto dodajDoktorDto)
    {
        var imiona = new Imiona
        {
            PierwszeImie = dodajDoktorDto.Imie,
            // Może być null
            DrugieImie = dodajDoktorDto.DrugieImie 
        };

        // Dodaj obiekt Imiona do kontekstu i zapisz zmiany, aby uzyskać IdImiona
        _context.Imiona.Add(imiona);
        await _context.SaveChangesAsync();

        var doktor = new Doktor
        {
            NrPrawaWykonywaniaZawodu = dodajDoktorDto.NrPrawaWykonywaniaZawodu,
            // Przypisz IdImiona do właściwości IdImion
            IdImion = imiona.IdImiona, 
            Nazwisko = dodajDoktorDto.Nazwisko,
            NrTelefonu = dodajDoktorDto.NrTelefonu,
            Pesel = dodajDoktorDto.Pesel,
            IdAdres = dodajDoktorDto.IdAdres
        };

        _context.Doktorzy.Add(doktor);
        await _context.SaveChangesAsync();

        // Ustawienie IdDoktor po zapisaniu
        doktor.IdDoktor = doktor.IdOsoba;
        _context.Doktorzy.Update(doktor);
        await _context.SaveChangesAsync();

        return new WyswietlDoktorDto
        {
            IdDoktor = doktor.IdDoktor,
            NrPrawaWykonywaniaZawodu = doktor.NrPrawaWykonywaniaZawodu,
            Imie = imiona.PierwszeImie,
            DrugieImie = imiona.DrugieImie,
            Nazwisko = doktor.Nazwisko,
            NrTelefonu = doktor.NrTelefonu,
            Pesel = doktor.Pesel,
            IdAdres = doktor.IdAdres
        };
    }

    public async Task<bool> UsunDoktora(int id)
    {
        var doktor = await _context.Doktorzy.FindAsync(id);
        if (doktor == null)
        {
            return false;
        }

        _context.Doktorzy.Remove(doktor);
        await _context.SaveChangesAsync();
        return true;
    }
}