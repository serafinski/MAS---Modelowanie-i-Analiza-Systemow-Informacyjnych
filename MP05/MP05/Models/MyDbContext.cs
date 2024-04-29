using Microsoft.EntityFrameworkCore;

namespace MP05.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // DOKTOR
        modelBuilder.Entity<Doktor>(e =>
        {
            e.HasKey(k => k.IdDoktor);
            e.Property(p => p.NumerPrawaWykonywaniaZawodu).HasMaxLength(7).IsRequired();
        });

        // LEK
        modelBuilder.Entity<Lek>(e =>
        {
            e.HasKey(k => k.IdLek);
            e.Property(p => p.NazwaChemiczna).HasMaxLength(50).IsRequired();
            e.Property(p => p.MaxDawkaDzienna).HasMaxLength(7).IsRequired();
        });

        // WIZYTA
        modelBuilder.Entity<Wizyta>(e =>
        {
            e.HasKey(k => k.IdWizyta);
            e.Property(p => p.DataWizyt).IsRequired();
            e.Property(p => p.OpisWizyt).HasMaxLength(255).IsRequired();
            
            //Relacja *-1 (wiele wizyt, 1 doktor)
            e.HasOne(e => e.Doktor)
                .WithMany(e => e.Wizyty)
                .HasForeignKey(e => e.IdDoktor)
                .OnDelete(DeleteBehavior.ClientCascade);
        });
    }
}