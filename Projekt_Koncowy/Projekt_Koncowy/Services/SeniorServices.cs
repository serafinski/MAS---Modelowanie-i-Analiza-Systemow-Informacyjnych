using Projekt_Koncowy.Data;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Services;

public interface ISeniorServices
{
    Task<SeniorWyswietlDto> DodajSenior(SeniorDodajDto seniorDodajDto);
    Task<bool> UsunSenior(int id);
}

public class SeniorServices : ISeniorServices
{
    private readonly MyDbContext _context;
    
    public SeniorServices(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<SeniorWyswietlDto> DodajSenior(SeniorDodajDto seniorDodajDto)
    {
        var imiona = new Imiona
        {
            PierwszeImie = seniorDodajDto.Imie,
            DrugieImie = seniorDodajDto.DrugieImie
        };

        _context.Imiona.Add(imiona);
        await _context.SaveChangesAsync();

        var adres = new Adres
        {
            Ulica = seniorDodajDto.Ulica,
            NrDomu = seniorDodajDto.NrDomu,
            NrMieszkania = seniorDodajDto.NrMieszkania,
            KodPocztowy = seniorDodajDto.KodPocztowy,
            Miejscowosc = seniorDodajDto.Miejscowosc
        };

        _context.Adresy.Add(adres);
        await _context.SaveChangesAsync();

        var senior = new Senior
        {
            RokPrzejsciaNaEmeryture = seniorDodajDto.RokPrzejsciaNaEmeryture,
            NrKontaktuAlarmowego = seniorDodajDto.NrKontaktuAlarmowego,
            IdImion = imiona.IdImiona,
            Nazwisko = seniorDodajDto.Nazwisko,
            NrTelefonu = seniorDodajDto.NrTelefonu,
            Pesel = seniorDodajDto.Pesel,
            IdAdres = adres.IdAdres
        };

        _context.Seniorzy.Add(senior);
        await _context.SaveChangesAsync();

        senior.IdSenior = senior.IdOsoba;
        _context.Seniorzy.Update(senior);
        await _context.SaveChangesAsync();

        return new SeniorWyswietlDto
        {
            IdPacjenta = senior.IdSenior,
            Imie = imiona.PierwszeImie,
            DrugieImie = imiona.DrugieImie,
            Nazwisko = senior.Nazwisko,
            NrKontaktuAlarmowego = senior.NrKontaktuAlarmowego,
            NrTelefonu = senior.NrTelefonu,
            Pesel = senior.Pesel,
            Ulica = adres.Ulica,
            NrDomu = adres.NrDomu,
            NrMieszkania = adres.NrMieszkania,
            KodPocztowy = adres.KodPocztowy,
            Miejscowosc = adres.Miejscowosc,
            RokPrzejsciaNaEmeryture = senior.RokPrzejsciaNaEmeryture
        };
    }
    
    public async Task<bool> UsunSenior(int id)
    {
        var senior = await _context.Seniorzy.FindAsync(id);
        if (senior == null)
        {
            return false;
        }

        var imiona = await _context.Imiona.FindAsync(senior.IdImion);
        var adres = await _context.Adresy.FindAsync(senior.IdAdres);

        _context.Seniorzy.Remove(senior);
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