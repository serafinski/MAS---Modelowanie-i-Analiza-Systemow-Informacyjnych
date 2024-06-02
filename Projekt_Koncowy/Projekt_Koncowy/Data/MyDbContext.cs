using Microsoft.EntityFrameworkCore;

namespace Projekt_Koncowy.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }
}