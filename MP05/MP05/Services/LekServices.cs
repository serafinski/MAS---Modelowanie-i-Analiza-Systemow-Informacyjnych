using MP05.Models;

namespace MP05.Services;


public interface ILekServices
{
    Task<Lek?> GetLek(int id);
    Task<Lek> AddLek(Lek lek);
    Task<bool> DeleteLek(int id);
}

public class LekServices : ILekServices
{
    private readonly MyDbContext _dbContext;

    public LekServices(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    //Zwracanie Leku po ID
    public async Task<Lek?> GetLek(int id)
    {
        return await _dbContext.Leki.FindAsync(id);
    }
    
    //Dodawanie Leku
    public async Task<Lek> AddLek(Lek lek)
    {
        //Dodanie Leku do bazy
        _dbContext.Leki.Add(lek);
        //Aktualizacja bazy danych
        await _dbContext.SaveChangesAsync();

        return lek;
    }
    
    //Usuwanie Leku po ID
    public async Task<bool> DeleteLek(int id)
    {
        var lek = await _dbContext.Leki.FindAsync(id);
        
        //Jeżeli lek nie istnieje — nic nie rób
        if (lek == null)
        {
            return false;
        }
        
        //Usuwanie Leku z bazy
        _dbContext.Remove(lek);
        //Aktualizacja bazy
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}