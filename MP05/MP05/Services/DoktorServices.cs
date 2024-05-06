using Microsoft.EntityFrameworkCore;
using MP05.Models;
using MP05.Models.DTOs;

namespace MP05.Services;

public interface IDoktorServices
{
    Task<Doktor?> GetDoktor(int id);
    Task<GetDoktorDto?> GetDtoDoktor(int id);
    Task<Doktor> AddDoktor(Doktor doktor);
    Task<bool> DeleteDoktor(int id);
}


public class DoktorServices : IDoktorServices
{
    private readonly MyDbContext _dbContext;

    public DoktorServices(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Zwracanie Doktora po ID
    // Będzie dodatkowy wiersz: "wizyty": null, bo lazy loading -> obejście przez DTO poniżej
    public async Task<Doktor?> GetDoktor(int id)
    {
        return await _dbContext.Doktorzy.FindAsync(id);
    }

    // Zwracanie Doktora po ID — obejście DTO
    public async Task<GetDoktorDto?> GetDtoDoktor(int id)
    {
        var doktor = await _dbContext.Doktorzy.FirstOrDefaultAsync(d => d.IdDoktor == id);

        if (doktor == null)
        {
            return null;
        }

        var dtoDoktor = new GetDoktorDto
        {
            IdOsoba = doktor.IdOsoba,
            Imie = doktor.Imie,
            Nazwisko = doktor.Nazwisko,
            Telefon = doktor.Telefon,
            Pesel = doktor.Pesel,
            IdDoktor = doktor.IdDoktor,
            NumerPrawaWykonywaniaZawodu = doktor.NumerPrawaWykonywaniaZawodu
        };

        return dtoDoktor;
    }
    
    //Dodawanie Doktora
    public async Task<Doktor> AddDoktor(Doktor doktor)
    {
        //Dodajemy Doktora do bazy
        _dbContext.Doktorzy.Add(doktor);
        //Aktualizacja bazy
        await _dbContext.SaveChangesAsync();

        return doktor;
    }
    
    //Usuwanie Doktora po ID
    public async Task<bool> DeleteDoktor(int id)
    {
        var doktor = await _dbContext.Doktorzy.FindAsync(id);
        
        //Jeżeli doktor nie istnieje — nic nie rób
        if (doktor == null)
        {
            return false;
        }
        
        //Usuwanie Doktora z bazy
        _dbContext.Remove(doktor);
        //Aktualizacja bazy
        await _dbContext.SaveChangesAsync();

        return true;
    }
}