using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Pacjent> Pacjenci { get; set; }
    public DbSet<Doktor> Doktorzy { get; set; }
    public DbSet<Wizyta> Wizyty { get; set; }
    public DbSet<Imiona> Imiona { get; set; }
    public DbSet<Adres> Adresy { get; set; }
    public DbSet<Lek> Leki { get; set; }
    public DbSet<Recepta> Recepty { get; set; }
    public DbSet<LekNaRecepcie> LekiNaRecepcie { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // //Konfiguracja dziedziczenia po Osobie
        // modelBuilder.Entity<Osoba>()
        //     .HasDiscriminator<string>("Discriminator")
        //     .HasValue<Osoba>("Osoba")
        //     .HasValue<Pacjent>("Pacjent")
        //     .HasValue<Doktor>("Doktor")
        //     .HasValue<Pielegniarka>("Pielegniarka");

        //Konfiguracja Osoba
        modelBuilder.Entity<Osoba>(e =>
        {
            e.HasKey(e => e.IdOsoba);

            e.Property(e => e.IdOsoba).ValueGeneratedOnAdd();

            e.Property(e => e.Nazwisko)
                .HasMaxLength(255)
                .IsRequired();

            e.Property(e => e.Pesel)
                .HasMaxLength(11)
                .IsRequired()
                .IsFixedLength();

            e.HasOne(e => e.Imiona)
                .WithMany(i => i.Osoby)
                .HasForeignKey(e => e.IdImion);

            e.HasOne(e => e.Adres)
                .WithMany(a => a.Osoby)
                .HasForeignKey(e => e.IdAdres);
        });

        //Konfiguracja dla Imiona
        modelBuilder.Entity<Imiona>(e =>
        {
            e.HasKey(e => e.IdImiona);

            e.Property(e => e.PierwszeImie)
                .HasMaxLength(50)
                .IsRequired();

            e.Property(e => e.DrugieImie)
                .HasMaxLength(50);

            e.HasData(new List<Imiona>
            {
                new Imiona
                {
                    IdImiona = 1,
                    PierwszeImie = "Jan",
                    DrugieImie = "Krzysztof"
                },
                new Imiona
                {
                    IdImiona = 2,
                    PierwszeImie = "Anna"
                }
            });
        });

        //Konfiguracja dla Adres
        modelBuilder.Entity<Adres>(e =>
        {
            e.HasKey(e => e.IdAdres);

            e.Property(e => e.Ulica)
                .HasMaxLength(255)
                .IsRequired();

            e.Property(e => e.NrDomu)
                .HasMaxLength(10)
                .IsRequired();

            e.Property(e => e.NrMieszkania)
                .HasMaxLength(6);

            e.Property(e => e.KodPocztowy)
                .HasMaxLength(6)
                .IsRequired();

            e.Property(e => e.Miejscowosc)
                .HasMaxLength(255)
                .IsRequired();

            e.HasData(new List<Adres>
            {
                new Adres
                {
                    IdAdres = 1,
                    Ulica = "Warszawska",
                    NrDomu = "1",
                    NrMieszkania = 15,
                    KodPocztowy = "00-001",
                    Miejscowosc = "Warszawa"
                },
                new Adres
                {
                    IdAdres = 2,
                    Ulica = "Krakowska",
                    NrDomu = "2",
                    KodPocztowy = "30-002",
                    Miejscowosc = "Kraków"
                }
            });
        });

        //Konfiguracja Doktor
        modelBuilder.Entity<Doktor>(e =>
        {
            e.ToTable("Doktorzy");
            
            e.Property(e => e.NrPrawaWykonywaniaZawodu)
                .HasMaxLength(7)
                .IsRequired();

            e.HasData(new List<Doktor>
            {
                new Doktor
                {
                    IdOsoba = 1,
                    IdImion = 1,
                    Nazwisko = "Kowalski",
                    Pesel = "80010112345",
                    IdAdres = 1,
                    NrPrawaWykonywaniaZawodu = "1234567"
                }
            });
        });

        //Konfiguracja Pielegniarka
        modelBuilder.Entity<Pielegniarka>(e =>
        {
            e.ToTable("Pielegniarki");
            
            e.Property(e => e.NrPrawaWykonywaniaZawodu)
                .HasMaxLength(7)
                .IsRequired();
        });

        //Konfiguracja Pacjent
        modelBuilder.Entity<Pacjent>(e =>
        {
            e.ToTable("Pacjenci");
            
            e.Property(e => e.NrKontaktuAlarmowego)
                .HasMaxLength(15)
                .IsRequired();


            e.HasData(new List<Pacjent>
            {
                new Pacjent
                {
                    IdOsoba = 2,
                    IdImion = 2,
                    Nazwisko = "Nowak",
                    Pesel = "90020267890",
                    IdAdres = 2,
                    NrKontaktuAlarmowego = "123456789"
                }
            });

        });

        //Konfiguracja Wizyta
        modelBuilder.Entity<Wizyta>(e =>
        {
            e.HasKey(e => e.IdWizyty);

            e.Property(e => e.OpisWizyty).HasMaxLength(255);

            e.HasOne(e => e.Pacjent)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdPacjent)
                .OnDelete(DeleteBehavior.NoAction);

            e.HasOne(e => e.Doktor)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdDoktor)
                .OnDelete(DeleteBehavior.NoAction);


            e.HasData(new List<Wizyta>
            {
                new Wizyta
                {
                    IdWizyty = 1,
                    DataWizyty = new DateTime(2024, 6, 2),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 2
                }
            });
        });

        //Konfiguracja Lek
        modelBuilder.Entity<Lek>(e =>
        {
            e.HasKey(e => e.IdLek);
            
            e.Property(e => e.NazwaLeku)
                .HasMaxLength(255)
                .IsRequired();

            e.HasData(new List<Lek>
            {
                new Lek
                {
                    IdLek = 1,
                    NazwaLeku = "Paracetamol"
                },
                new Lek
                {
                    IdLek = 2,
                    NazwaLeku = "Ibuprofen"
                }
            });
        });

        //Konfiguracja Recepta
        modelBuilder.Entity<Recepta>(e =>
        {
            e.HasKey(e => e.IdRecepta);
            
            e.Property(e => e.DataWystawienia)
                .IsRequired();

            e.HasOne(e => e.Wizyta)
                .WithMany(w => w.Recepty)
                .HasForeignKey(e => e.IdWizyta)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasData(new List<Recepta>
            {
                new Recepta
                {
                    IdRecepta = 1,
                    DataWystawienia = new DateTime(2024, 6, 2),
                    IdWizyta = 1
                }
            });
        });

        //Konfiguracja LekNaRecepcie
        modelBuilder.Entity<LekNaRecepcie>(e =>
        {
            e.HasKey(e => new { e.IdLek, e.IdRecepta });
            
            e.Property(e => e.Ilosc)
                .HasMaxLength(255)
                .IsRequired();
            
            e.Property(e => e.Dawkowanie)
                .HasMaxLength(255)
                .IsRequired();
                
            e.HasOne(e => e.Lek)
                .WithMany(l => l.LekiNaRecepcie)
                .HasForeignKey(e => e.IdLek)
                .OnDelete(DeleteBehavior.Cascade);
                
            e.HasOne(e => e.Recepta)
                .WithMany(r => r.LekiNaRecepcie)
                .HasForeignKey(e => e.IdRecepta)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasData(new List<LekNaRecepcie>
            {
                new LekNaRecepcie
                {
                    IdLek = 1,
                    IdRecepta = 1,
                    Ilosc = "2 tabletki",
                    Dawkowanie = "3x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 2,
                    IdRecepta = 1,
                    Ilosc = "1 tabletka",
                    Dawkowanie = "2x dziennie"
                }
            });
        });
    }
}