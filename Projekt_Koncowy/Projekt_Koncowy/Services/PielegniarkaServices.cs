using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface IPielegniarkaServices
{
    Task<PielegniarkaWyswietlDto> DodajPielegniarke(PielegniarkaDodajDto dto);
    Task<bool> UsunPielegniarke(int id);
    Task<string> WyswietlGrafik(int id);
}

public class PielegniarkaServices : IPielegniarkaServices
{
    private readonly MyDbContext _context;

    public PielegniarkaServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<PielegniarkaWyswietlDto> DodajPielegniarke(PielegniarkaDodajDto pielegniarkaDodajDto)
    {
        var imiona = new Imiona
        {
            PierwszeImie = pielegniarkaDodajDto.Imie,
            //Może być null
            DrugieImie = pielegniarkaDodajDto.DrugieImie
        };
        
        // Dodaj obiekt Imiona do kontekstu i zapisz zmiany, aby uzyskać IdImiona
        _context.Imiona.Add(imiona);
        await _context.SaveChangesAsync();

        var adres = new Adres
        {
            Ulica = pielegniarkaDodajDto.Ulica,
            NrDomu = pielegniarkaDodajDto.NrDomu,
            NrMieszkania = pielegniarkaDodajDto.NrMieszkania,
            KodPocztowy = pielegniarkaDodajDto.KodPocztowy,
            Miejscowosc = pielegniarkaDodajDto.Miejscowosc
        };
        
        _context.Adresy.Add(adres);
        await _context.SaveChangesAsync();
        
        var pielegniarka = new Pielegniarka
        {
            NrPrawaWykonywaniaZawodu = pielegniarkaDodajDto.NrPrawaWykonywaniaZawodu,
            // Przypisz IdImiona do właściwości IdImion
            IdImion = imiona.IdImiona,
            Nazwisko = pielegniarkaDodajDto.Nazwisko,
            NrTelefonu = pielegniarkaDodajDto.NrTelefonu,
            Pesel = pielegniarkaDodajDto.Pesel,
            IdAdres = adres.IdAdres,
            Grafik = pielegniarkaDodajDto.Grafik,
        };

        _context.Pielegniarki.Add(pielegniarka);
        await _context.SaveChangesAsync();
        
        //Ustawienie IdPielegniarki po zapisaniu
        pielegniarka.IdPielegniarka = pielegniarka.IdOsoba;
        _context.Pielegniarki.Update(pielegniarka);
        await _context.SaveChangesAsync();

        return new PielegniarkaWyswietlDto
        {
            IdPielegniarka = pielegniarka.IdPielegniarka,
            NrPrawaWykonywaniaZawodu = pielegniarka.NrPrawaWykonywaniaZawodu,
            Imie = imiona.PierwszeImie,
            DrugieImie = imiona.DrugieImie,
            Nazwisko = pielegniarka.Nazwisko,
            NrTelefonu = pielegniarka.NrTelefonu,
            Pesel = pielegniarka.Pesel,
            Ulica = adres.Ulica,
            NrDomu = adres.NrDomu,
            NrMieszkania = adres.NrMieszkania,
            KodPocztowy = adres.KodPocztowy,
            Miejscowosc = adres.Miejscowosc,
            Grafik = pielegniarka.Grafik
        };
    }

    public async Task<bool> UsunPielegniarke(int id)
    {
        var pielegniarka = await _context.Pielegniarki.FindAsync(id);
        if (pielegniarka == null)
        {
            return false;
        }
        
        var imiona = await _context.Imiona.FindAsync(pielegniarka.IdImion);
        var adres = await _context.Adresy.FindAsync(pielegniarka.IdAdres);
        
        _context.Pielegniarki.Remove(pielegniarka);
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

    public async Task<string> WyswietlGrafik(int id)
    {
        var pielegniarka = await _context.Pielegniarki.FindAsync(id);
        return pielegniarka?.Grafik;
    }
}
