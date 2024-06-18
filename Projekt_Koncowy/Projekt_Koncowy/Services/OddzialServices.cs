using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Koncowy.Services;

public interface IOddzialServices
{
    Task<OddzialResponseDto> DodajOddzial(OddzialDodajDto oddzialDodajDto);
    Task<bool> UsunOddzial(int id);
    Task<OddzialWyswietlDto> WyswietlOddzial(int id);
    Task<List<OddzialyWyswietlDto>> WyswietlWszystkieOddzialy(int idPlacowki);
}

public class OddzialServices : IOddzialServices
{
    private readonly MyDbContext _context;

    public OddzialServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<OddzialResponseDto> DodajOddzial(OddzialDodajDto oddzialDodajDto)
    {
        var oddzial = new Oddzial
        {
            NazwaOddzial = oddzialDodajDto.NazwaOddzial,
            IdPlacowki = oddzialDodajDto.IdPlacowki
        };

        _context.Oddzialy.Add(oddzial);
        await _context.SaveChangesAsync();

        // Pobierz dane o dodanym oddziale z nazwą placówki
        var dodanyOddzial = await _context.Oddzialy.Include(o => o.Placowka)
            .FirstOrDefaultAsync(o => o.IdOddzial == oddzial.IdOddzial);

        if (dodanyOddzial == null)
        {
            throw new Exception("Błąd podczas pobierania dodanego oddziału.");
        }

        return new OddzialResponseDto
        {
            IdOddzial = dodanyOddzial.IdOddzial,
            NazwaOddzial = dodanyOddzial.NazwaOddzial,
            IdPlacowki = dodanyOddzial.IdPlacowki,
            NazwaPlacowki = dodanyOddzial.Placowka.Nazwa
        };
    }

    public async Task<bool> UsunOddzial(int id)
    {
        var oddzial = await _context.Oddzialy.FindAsync(id);
        if (oddzial == null)
        {
            return false;
        }

        _context.Oddzialy.Remove(oddzial);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<OddzialWyswietlDto> WyswietlOddzial(int id)
    {
        var oddzial = await _context.Oddzialy.Include(o => o.Placowka)
            .FirstOrDefaultAsync(o => o.IdOddzial == id);
        if (oddzial == null) return null;

        return new OddzialWyswietlDto
        {
            NazwaOddzial = oddzial.NazwaOddzial,
            IdPlacowki = oddzial.IdPlacowki,
            NazwaPlacowki = oddzial.Placowka.Nazwa
        };
    }

    public async Task<List<OddzialyWyswietlDto>> WyswietlWszystkieOddzialy(int idPlacowki)
    {
        return await _context.Oddzialy
            .Where(o => o.IdPlacowki == idPlacowki)
            .Select(o => new OddzialyWyswietlDto
            {
                IdOddzial = o.IdOddzial,
                NazwaOddzial = o.NazwaOddzial
            }).ToListAsync();
    }
}