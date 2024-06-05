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

        var pielegniarka = new Pielegniarka
        {
            NrPrawaWykonywaniaZawodu = pielegniarkaDodajDto.NrPrawaWykonywaniaZawodu,
            // Przypisz IdImiona do właściwości IdImion
            IdImion = imiona.IdImiona,
            Nazwisko = pielegniarkaDodajDto.Nazwisko,
            NrTelefonu = pielegniarkaDodajDto.NrTelefonu,
            Pesel = pielegniarkaDodajDto.Pesel,
            IdAdres = pielegniarkaDodajDto.IdAdres,
            Grafik = pielegniarkaDodajDto.Grafik
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
            IdAdres = pielegniarka.IdAdres,
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

        _context.Pielegniarki.Remove(pielegniarka);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> WyswietlGrafik(int id)
    {
        var pielegniarka = await _context.Pielegniarki.FindAsync(id);
        return pielegniarka?.Grafik;
    }
}
