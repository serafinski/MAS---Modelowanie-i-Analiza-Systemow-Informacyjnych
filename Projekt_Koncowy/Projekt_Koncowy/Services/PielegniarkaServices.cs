using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Projekt_Koncowy.Services;

public interface IPielegniarkaServices
{
    Task<PielegniarkaResponseDto> DodajPielegniarke(DodajPielegniarkaDto pielegniarkaDto);
    Task<bool> UsunPielegniarke(int id);
    Task<WyswietlPielegniarkeDto?> WyswietlDane(int id);
    Task<string> WyswietlGrafik(int id);
}

public class PielegniarkaServices : IPielegniarkaServices
{
    private readonly MyDbContext _context;

    public PielegniarkaServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<PielegniarkaResponseDto> DodajPielegniarke(DodajPielegniarkaDto dodajPielegniarkaDto)
    {
        var imiona = await _context.Imiona.FindAsync(dodajPielegniarkaDto.IdImion);
        var adres = await _context.Adresy.FindAsync(dodajPielegniarkaDto.IdAdres);

        if (imiona == null || adres == null)
        {
            throw new ArgumentException("Blędne IdImion lub IdAdres");
        }

        var pielegniarka = new Pielegniarka
        {
            IdImion = dodajPielegniarkaDto.IdImion,
            Nazwisko = dodajPielegniarkaDto.Nazwisko,
            Pesel = dodajPielegniarkaDto.Pesel,
            IdAdres = dodajPielegniarkaDto.IdAdres,
            NrPrawaWykonywaniaZawodu = dodajPielegniarkaDto.NrPrawaWykonywaniaZawodu,
            Grafik = dodajPielegniarkaDto.Grafik,
            Imiona = imiona,
            Adres = adres,
            NrTelefonu = dodajPielegniarkaDto.NrTelefonu
        };

        _context.Pielegniarki.Add(pielegniarka);
        await _context.SaveChangesAsync();
            
        // Przypisanie IdOsoba do IdPielegniarka
        pielegniarka.IdPielegniarka = pielegniarka.IdOsoba;
        _context.Pielegniarki.Update(pielegniarka);
        await _context.SaveChangesAsync();

        var imionaList = new List<string> { pielegniarka.Imiona.PierwszeImie };
        if (!string.IsNullOrEmpty(pielegniarka.Imiona.DrugieImie))
        {
            imionaList.Add(pielegniarka.Imiona.DrugieImie);
        }

        var adresString = $"{pielegniarka.Adres.Ulica} {pielegniarka.Adres.NrDomu}";
        if (pielegniarka.Adres.NrMieszkania.HasValue)
        {
            adresString += $"/{pielegniarka.Adres.NrMieszkania}";
        }
        adresString += $", {pielegniarka.Adres.KodPocztowy} {pielegniarka.Adres.Miejscowosc}";

        return new PielegniarkaResponseDto
        {
            IdOsoba = pielegniarka.IdOsoba,
            IdPielegniarka = pielegniarka.IdPielegniarka,
            Nazwisko = pielegniarka.Nazwisko,
            Pesel = pielegniarka.Pesel,
            Imiona = imionaList,
            Adres = adresString,
            NrPrawaWykonywaniaZawodu = pielegniarka.NrPrawaWykonywaniaZawodu,
            Grafik = pielegniarka.Grafik,
            NrTelefonu = pielegniarka.NrTelefonu
        };
    }

    public async Task<bool> UsunPielegniarke(int id)
    {
        var pielegniarka = await _context.Pielegniarki.FindAsync(id);
        if (pielegniarka == null)
        {
            return false;
        }

        _context.Pielegniarki.Remove(pielegniarka);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<WyswietlPielegniarkeDto?> WyswietlDane(int id)
    {
        var pielegniarka = await _context.Pielegniarki
            .Include(p => p.Imiona)
            .Include(p => p.Adres)
            .FirstOrDefaultAsync(p => p.IdOsoba == id);
    
        if (pielegniarka == null)
        {
            return null;
        }
    
        var imiona = new List<string> { pielegniarka.Imiona.PierwszeImie };
        if (!string.IsNullOrEmpty(pielegniarka.Imiona.DrugieImie))
        {
            imiona.Add(pielegniarka.Imiona.DrugieImie);
        }
    
        var adres = $"{pielegniarka.Adres.Ulica} {pielegniarka.Adres.NrDomu}";
        if (pielegniarka.Adres.NrMieszkania.HasValue)
        {
            adres += $"/{pielegniarka.Adres.NrMieszkania}";
        }
        adres += $", {pielegniarka.Adres.KodPocztowy} {pielegniarka.Adres.Miejscowosc}";
    
        return new WyswietlPielegniarkeDto
        {
            IdOsoba = pielegniarka.IdOsoba,
            Nazwisko = pielegniarka.Nazwisko,
            Pesel = pielegniarka.Pesel,
            Imiona = imiona,
            Adres = adres,
            NrPrawaWykonywaniaZawodu = pielegniarka.NrPrawaWykonywaniaZawodu,
            Grafik = pielegniarka.Grafik,
            NrTelefonu = pielegniarka.NrTelefonu
        };
    }

    public async Task<string> WyswietlGrafik(int id)
    {
        var pielegniarka = await _context.Pielegniarki.FindAsync(id);
        return pielegniarka?.Grafik;
    }
}
