using Microsoft.EntityFrameworkCore;
using MP05.Models;
using MP05.Models.DTOs;

namespace MP05.Services;

public interface IWizytaServices
{
    Task<Wizyta?> GetWizyta(int id);
    Task<GetWizytaDto> GetDtoWizyta(int id);
    Task<Wizyta> AddWizyta(Wizyta wizyta);
    Task<bool> DeleteWizyta(int id);
}

public class WizytaServices : IWizytaServices
{
    private readonly MyDbContext _dbContext;

    public WizytaServices(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Zwracanie Wizyty po ID
    // Będzie dodatkowy wiersz: "doktor": null, bo lazy loading -> obejście przez DTO poniżej
    public async Task<Wizyta?> GetWizyta(int id)
    {
        return await _dbContext.Wizyty.FindAsync(id);
    }
    
    // Zwracanie Wizyty po ID — obejście DTO
    public async Task<GetWizytaDto> GetDtoWizyta(int id)
    {
        var wizyta = await _dbContext.Wizyty.FirstOrDefaultAsync(w => w.IdWizyta == id);

        if (wizyta == null)
        {
            return null;
        }

        var dtoWizyta = new GetWizytaDto
        {
            IdWizyta = wizyta.IdWizyta,
            DataWizyty = wizyta.DataWizyty,
            OpisWizyty = wizyta.OpisWizyty,
            IdDoktor = wizyta.IdDoktor
        };
        
        return dtoWizyta;
    }
    
    
    //Dodawanie Wizyty
    public async Task<Wizyta> AddWizyta(Wizyta wizyta)
    {
        //Dodajemy Wizyte do bazy
        _dbContext.Wizyty.Add(wizyta);
        //Aktualizacja bazy
        await _dbContext.SaveChangesAsync();

        return wizyta;
    }
    
    //Usuwanie Wizyty po ID
    public async Task<bool> DeleteWizyta(int id)
    {
        var wizyta = await _dbContext.Wizyty.FindAsync(id);
        
        //Jeżeli wizyta nie istnieje — nic nie rób
        if (wizyta == null)
        {
            return false;
        }
        
        //Usuwanie Wizyty z bazy
        _dbContext.Remove(wizyta);
        //Aktualizacja bazy
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}