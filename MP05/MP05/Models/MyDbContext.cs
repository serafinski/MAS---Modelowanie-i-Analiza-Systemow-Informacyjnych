using Microsoft.EntityFrameworkCore;

namespace MP05.Models;


//Po każdej modyfikacji pliku trzeba zrobić migracje i update bazy!
//Pamiętać o odświeżeniu bazy będą na dbo/@localhost
        
// Tworzymy migracje za pomocą
// dotnet ef migrations add Init
// ALE musimy znajdować się w folderze niżej z naszą solucją
        
// Aktualizujemy bazę danych za pomocą
// dotnet ef database update
        
//Jak coś zrobimy z _EFMigrations History -> to najprostszym sposobem na ogarnięcie tego jest
//usunięcie wszystkich tabelek i foldera migracji i odpalenie jeszcze raz tworzenia migracji
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    //Rzutowanie tabel na listy
    public DbSet<Doktor> Doktorzy { get; set; }
    public DbSet<Lek> Leki { get; set; }
    public DbSet<Wizyta> Wizyty { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Osoba>(e =>
        {
            e.HasKey(o=>o.IdOsoba);
            e.ToTable("Osoby");
        });
        
        // DOKTOR
        modelBuilder.Entity<Doktor>(e =>
        {
            e.ToTable("Doktorzy");
            e.Property(d => d.IdDoktor).ValueGeneratedOnAdd();
            e.Property(p => p.NumerPrawaWykonywaniaZawodu).HasMaxLength(7).IsRequired();
            
            // Dane Doktorów
            e.HasData(new List<Doktor>
            {
                new Doktor
                {
                    IdDoktor = 1,
                    NumerPrawaWykonywaniaZawodu = "7654321",
                    IdOsoba = 1,
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    Telefon = "123 456 789",
                    Pesel = "71031678901"
                }
            });
        });

        // LEK
        modelBuilder.Entity<Lek>(e =>
        {
            e.HasKey(k => k.IdLek);
            e.Property(p => p.NazwaChemiczna).HasMaxLength(50).IsRequired();
            e.Property(p => p.MaxDawkaDzienna).HasMaxLength(7).IsRequired();
            
            // Dane Leków
            e.HasData(new List<Lek>
            {
                new Lek
                {
                    IdLek = 1,
                    NazwaChemiczna = "Ibuprofen",
                    MaxDawkaDzienna = "1200mg"
                },
                new Lek
                {
                    IdLek = 2,
                    NazwaChemiczna = "Aspiryna",
                    MaxDawkaDzienna = "1000mg"
                }
            });
        });

        // WIZYTA
        modelBuilder.Entity<Wizyta>(e =>
        {
            e.HasKey(k => k.IdWizyta);
            e.Property(p => p.DataWizyty).IsRequired();
            e.Property(p => p.OpisWizyty).HasMaxLength(255).IsRequired();
            
            //Relacja *-1 (wiele wizyt, 1 doktor)
            e.HasOne(e => e.Doktor)
                .WithMany(e => e.Wizyty)
                .HasForeignKey(e => e.IdDoktor)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Dane Wizyt
            e.HasData(new List<Wizyta>
            {
                new Wizyta
                {
                    IdWizyta = 1,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1
                }
            });
        });
    }
}