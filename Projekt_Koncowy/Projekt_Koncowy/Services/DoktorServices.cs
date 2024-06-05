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

        var doktor = new Doktor
        {
            NrPrawaWykonywaniaZawodu = doktorDodajDto.NrPrawaWykonywaniaZawodu,
            // Przypisz IdImiona do właściwości IdImion
            IdImion = imiona.IdImiona, 
            Nazwisko = doktorDodajDto.Nazwisko,
            NrTelefonu = doktorDodajDto.NrTelefonu,
            Pesel = doktorDodajDto.Pesel,
            IdAdres = doktorDodajDto.IdAdres
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