using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Adres> Adresy { get; set; }
    public DbSet<Doktor> Doktorzy { get; set; }
    public DbSet<Dorosly> Dorosli { get; set; }
    public DbSet<Dziecko> Dzieci { get; set; }
    public DbSet<Imiona> Imiona { get; set; }
    public DbSet<KierownikPlacowki> KierownicyPlacowek { get; set; }
    public DbSet<Lek> Leki { get; set; }
    public DbSet<LekNaRecepcie> LekiNaRecepcie { get; set; }
    public DbSet<Oddzial> Oddzialy { get; set; }
    public DbSet<Osoba> Osoby { get; set; }
    public DbSet<Pacjent> Pacjenci { get; set; }
    public DbSet<Pielegniarka> Pielegniarki { get; set; }
    public DbSet<Placowka> Placowki { get; set; }
    public DbSet<Recepta> Recepty { get; set; }
    public DbSet<Senior> Seniorzy { get; set; }
    public DbSet<Wizyta> Wizyty { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Konfiguracja Placowka
        modelBuilder.Entity<Placowka>(e =>
        {
            e.HasKey(p => p.IdPlacowka);
            e.Property(p => p.Nazwa)
                .HasMaxLength(255)
                .IsRequired();

            e.HasMany(p => p.Kierownicy)
                .WithOne(k => k.Placowka)
                .HasForeignKey(k => k.IdPlacowki);

            e.HasMany(p => p.Wizyty)
                .WithOne(w => w.Placowka)
                .HasForeignKey(w => w.IdPlacowka);

            e.HasMany(p => p.Oddzialy)
                .WithOne(o => o.Placowka)
                .HasForeignKey(o => o.IdPlacowki)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            e.HasData(new List<Placowka>
            {
                new Placowka
                {
                    IdPlacowka = 1,
                    Nazwa = "Szpital Miejski"
                },
                new Placowka
                {
                    IdPlacowka = 2,
                    Nazwa = "Klinika Specjalistyczna"
                }
            });
        });
        
        modelBuilder.Entity<Oddzial>(e =>
        {
            e.HasKey(o => o.IdOddzial);
            e.Property(o => o.NazwaOddzial)
                .HasMaxLength(255)
                .IsRequired();

            e.HasOne(o => o.Placowka)
                .WithMany(p => p.Oddzialy)
                .HasForeignKey(o => o.IdPlacowki)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            e.HasData(new List<Oddzial>
            {
                new Oddzial
                {
                    IdOddzial = 1,
                    NazwaOddzial = "Kardiologia",
                    IdPlacowki = 2
                },
                new Oddzial
                {
                    IdOddzial = 2,
                    NazwaOddzial = "Neurologia",
                    IdPlacowki = 2
                }
            });
        });
        
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
            
            //OGRANICZENIE ATRYBUTU
            e.Property(e => e.NrTelefonu)
                .HasMaxLength(16)
                .IsRequired();
            
            //OGRANICZENIE UNIQUE
            e.HasIndex(e => e.Pesel).IsUnique();

            e.HasOne(e => e.Imiona)
                .WithMany(i => i.Osoby)
                .HasForeignKey(e => e.IdImion)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(e => e.Adres)
                .WithMany(a => a.Osoby)
                .HasForeignKey(e => e.IdAdres)
                .OnDelete(DeleteBehavior.Cascade);
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
                    PierwszeImie = "Anna",
                    DrugieImie = "Ewa"
                },
                new Imiona
                {
                    IdImiona = 3,
                    PierwszeImie = "Piotr"
                },
                new Imiona
                {
                    IdImiona = 4,
                    PierwszeImie = "Maria"
                },
                new Imiona
                {
                    IdImiona = 5,
                    PierwszeImie = "Michał"
                },
                new Imiona
                {
                    IdImiona = 6,
                    PierwszeImie = "Anna",
                    DrugieImie = "Maria"
                },
                new Imiona
                {
                    IdImiona = 7,
                    PierwszeImie = "Katarzyna",
                },
                new Imiona
                {
                    IdImiona = 8,
                    PierwszeImie = "Piotr"
                },
                new Imiona
                {
                    IdImiona = 9,
                    PierwszeImie = "Agnieszka"
                },
                new Imiona
                {
                    IdImiona = 10,
                    PierwszeImie = "Tomasz",
                    DrugieImie = "Paweł"
                },
                new Imiona
                {
                    IdImiona = 11,
                    PierwszeImie = "Julia"
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
                },
                new Adres
                {
                    IdAdres = 3,
                    Ulica = "Gdanska",
                    NrDomu = "3",
                    NrMieszkania = 3,
                    KodPocztowy = "80-003",
                    Miejscowosc = "Gdansk"
                },
                new Adres
                {
                    IdAdres = 4,
                    Ulica = "Skierniewicka",
                    NrDomu = "4",
                    KodPocztowy = "96-100",
                    Miejscowosc = "Skierniewice"
                },
                new Adres
                {
                    IdAdres = 5,
                    Ulica = "Poznańska",
                    NrDomu = "5",
                    NrMieszkania = 10,
                    KodPocztowy = "60-005",
                    Miejscowosc = "Poznań"
                },
                new Adres
                {
                    IdAdres = 6,
                    Ulica = "Wrocławska",
                    NrDomu = "6",
                    KodPocztowy = "50-006",
                    Miejscowosc = "Wrocław"
                },
                new Adres
                {
                    IdAdres = 7,
                    Ulica = "Łódzka",
                    NrDomu = "7",
                    NrMieszkania = 20,
                    KodPocztowy = "90-007",
                    Miejscowosc = "Łódź"
                },
                new Adres
                {
                    IdAdres = 8,
                    Ulica = "Katowicka",
                    NrDomu = "8",
                    KodPocztowy = "40-008",
                    Miejscowosc = "Katowice"
                },
                new Adres
                {
                    IdAdres = 9,
                    Ulica = "Lubliniecka",
                    NrDomu = "9",
                    NrMieszkania = 5,
                    KodPocztowy = "20-009",
                    Miejscowosc = "Lublin"
                },
                new Adres
                {
                    IdAdres = 10,
                    Ulica = "Białostocka",
                    NrDomu = "10",
                    KodPocztowy = "15-010",
                    Miejscowosc = "Białystok"
                },
                new Adres
                {
                    IdAdres = 11,
                    Ulica = "Szczecińska",
                    NrDomu = "11",
                    NrMieszkania = 11,
                    KodPocztowy = "70-011",
                    Miejscowosc = "Szczecin"
                }
            });
        });

        //Konfiguracja Doktor
        modelBuilder.Entity<Doktor>(e =>
        {
            e.ToTable("Doktorzy");
        
            e.HasBaseType<Osoba>();
        
            e.Property(e => e.NrPrawaWykonywaniaZawodu)
                .HasMaxLength(7)
                .IsRequired();
        
            e.HasIndex(e => e.NrPrawaWykonywaniaZawodu).IsUnique();
            
            e.HasData(new List<Doktor>
            {
                new Doktor
                {
                    IdOsoba = 1,
                    IdImion = 1,
                    Nazwisko = "Kowalski",
                    Pesel = "80010112345",
                    IdAdres = 1,
                    NrPrawaWykonywaniaZawodu = "1234567",
                    NrTelefonu = "791 123 456"
                },
                new Doktor
                {
                    IdOsoba = 4,
                    IdImion = 4,
                    Nazwisko = "Wiśniewska",
                    Pesel = "95030378901",
                    IdAdres = 4,
                    NrPrawaWykonywaniaZawodu = "7654321",
                    NrTelefonu = "698 987 654"
                }
            });
        });
        
        // Konfiguracja KierownikPlacowki
        modelBuilder.Entity<KierownikPlacowki>(e =>
        {
            e.HasBaseType<Doktor>();

            e.Property(k => k.DataObjeciaStanowiska).IsRequired();
            
            //By usunąć placówkę - trzeba usunąć kierownika
            e.HasOne(k => k.Placowka)
                .WithMany(p => p.Kierownicy)
                .HasForeignKey(k => k.IdPlacowki)
                .OnDelete(DeleteBehavior.Restrict);

            // Indeks dla kierownika w danej placowce
            e.HasIndex(k => new { k.IdOsoba, k.IdPlacowki }).IsUnique();
            
            e.HasData(new List<KierownikPlacowki>
            {
                new KierownikPlacowki
                {
                    IdOsoba = 11,
                    IdImion = 11,
                    IdAdres = 11,
                    Nazwisko = "Serafinska",
                    Pesel = "81061868372",
                    NrPrawaWykonywaniaZawodu = "1730501",
                    IdPlacowki = 2,
                    DataObjeciaStanowiska = new DateTime(2021, 2, 1),
                    NrTelefonu = "602 345 678"
                }
            });
        });
        
        //Konfiguracja Pacjent
        modelBuilder.Entity<Pacjent>(e =>
        {
            e.ToTable("Pacjenci");
            
            e.HasBaseType<Osoba>();
            
            e.Property(e => e.NrKontaktuAlarmowego)
                .HasMaxLength(16)
                .IsRequired();
        });
        
        //Konfiguracja Dorosly
        modelBuilder.Entity<Dorosly>(e =>
        {
            e.Property(e => e.NipPracodawcy)
                .HasMaxLength(11)
                .IsRequired();
            
            e.HasData(new List<Dorosly>
            {
                new Dorosly
                {
                    IdOsoba = 2,
                    IdImion = 2,
                    Nazwisko = "Nowak",
                    Pesel = "80110112346",
                    IdAdres = 2,
                    NrKontaktuAlarmowego = "123456789",
                    NipPracodawcy = "1070041074",
                    NrTelefonu = "785 654 321"
                },
                new Dorosly
                {
                    IdOsoba = 3,
                    IdImion = 3,
                    Nazwisko = "Zieliński",
                    Pesel = "90020267890",
                    IdAdres = 3,
                    NrKontaktuAlarmowego = "987654321",
                    NipPracodawcy = "1070041074",
                    NrTelefonu = "691 234 567"
                }
            });
        });
        
        //Konfiguracja Senior
        modelBuilder.Entity<Senior>(e =>
        {
            e.Property(e => e.RokPrzejsciaNaEmeryture)
                .HasMaxLength(4)
                .IsRequired();

            e.HasData(new List<Senior>
            {
                new Senior
                {
                    IdOsoba = 5,
                    IdImion = 5,
                    Nazwisko = "Nowicki",
                    Pesel = "65010112345",
                    IdAdres = 5,
                    NrKontaktuAlarmowego = "123456789",
                    RokPrzejsciaNaEmeryture = 2010,
                    NrTelefonu = "604 876 543"
                },
                new Senior
                {
                    IdOsoba = 6,
                    IdImion = 6,
                    Nazwisko = "Kowalska",
                    Pesel = "60030378901",
                    IdAdres = 6,
                    NrKontaktuAlarmowego = "987654321",
                    RokPrzejsciaNaEmeryture = 2005,
                    NrTelefonu = "723 567 890"
                }
            });
        });
        
        //Konfiguracja Dziecko
        modelBuilder.Entity<Dziecko>(e =>
        {
            e.Property(e => e.NazwaSzkoly)
                .IsRequired()
                .HasMaxLength(255);
            
            e.HasData(new List<Dziecko>
            {
                new Dziecko
                {
                    IdOsoba = 7,
                    IdImion = 7,
                    Nazwisko = "Zielinska",
                    Pesel = "12010112345",
                    IdAdres = 7,
                    NrKontaktuAlarmowego = "456123789",
                    NazwaSzkoly = "Szkoła Podstawowa nr 1",
                    NrTelefonu = "786 345 123"
                },
                new Dziecko
                {
                    IdOsoba = 8,
                    IdImion = 8,
                    Nazwisko = "Wojcik",
                    Pesel = "13020267890",
                    IdAdres = 8,
                    NrKontaktuAlarmowego = "789654321",
                    NazwaSzkoly = "Szkoła Podstawowa nr 2",
                    NrTelefonu = "693 876 234"
                }
            });
        });
        
        //Konfiguracja Wizyta
        modelBuilder.Entity<Wizyta>(e =>
        {
            e.HasKey(e => e.IdWizyty);

            e.Property(e => e.OpisWizyty);

            e.HasOne(e => e.Pacjent)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdPacjent)
                .OnDelete(DeleteBehavior.NoAction);
            
            //ASOCJACJA ZWYKŁA
            e.HasOne(e => e.Doktor)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdDoktor)
                .OnDelete(DeleteBehavior.NoAction);

            e.HasOne(e => e.Placowka)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdPlacowka)
                .OnDelete(DeleteBehavior.NoAction);

            e.HasData(new List<Wizyta>
            {
                new Wizyta
                {
                    IdWizyty = 1,
                    DataWizyty = new DateTime(2024, 6, 2),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 2,
                    IdPlacowka = 1
                },
                new Wizyta
                {
                    IdWizyty = 2,
                    DataWizyty = new DateTime(2024, 6, 3),
                    OpisWizyty = "Wizyta specjalistyczna",
                    IdDoktor = 4,
                    IdPacjent = 2,
                    IdPlacowka = 2
                },
                new Wizyta
                {
                    IdWizyty = 3,
                    DataWizyty = new DateTime(2024, 6, 4),
                    OpisWizyty = "Wizyta rutynowa",
                    IdDoktor = 1,
                    IdPacjent = 3,
                    IdPlacowka = 1
                },
                new Wizyta
                {
                    IdWizyty = 4,
                    DataWizyty = new DateTime(2024, 6, 5),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 7,
                    IdPlacowka = 1
                },
                new Wizyta
                {
                    IdWizyty = 5,
                    DataWizyty = new DateTime(2024, 6, 6),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 8,
                    IdPlacowka = 2
                },
                new Wizyta
                {
                    IdWizyty = 6,
                    DataWizyty = new DateTime(2024, 6, 7),
                    OpisWizyty = "Wizyta specjalistyczna",
                    IdDoktor = 1,
                    IdPacjent = 5,
                    IdPlacowka = 1
                },
                new Wizyta
                {
                    IdWizyty = 7,
                    DataWizyty = new DateTime(2024, 6, 8),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 6,
                    IdPlacowka = 2
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
                },
                new Lek
                {
                    IdLek = 3,
                    NazwaLeku = "Aspirin"
                },
                new Lek
                {
                    IdLek = 4,
                    NazwaLeku = "Metformin"
                },
                new Lek
                {
                    IdLek = 5,
                    NazwaLeku = "Amoxicillin"
                },
                new Lek
                {
                    IdLek = 6,
                    NazwaLeku = "Lisinopril"
                },
                new Lek
                {
                    IdLek = 7,
                    NazwaLeku = "Omeprazole"
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
                },
                new Recepta
                {
                    IdRecepta = 2,
                    DataWystawienia = new DateTime(2024, 6, 3),
                    IdWizyta = 2
                },
                new Recepta
                {
                    IdRecepta = 3,
                    DataWystawienia = new DateTime(2024, 6, 4),
                    IdWizyta = 3
                },
                new Recepta
                {
                    IdRecepta = 4,
                    DataWystawienia = new DateTime(2024, 6, 5),
                    IdWizyta = 4
                },
                new Recepta
                {
                    IdRecepta = 5,
                    DataWystawienia = new DateTime(2024, 6, 6),
                    IdWizyta = 5
                },
                new Recepta
                {
                    IdRecepta = 6,
                    DataWystawienia = new DateTime(2024, 6, 7),
                    IdWizyta = 6
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
                },
                new LekNaRecepcie
                {
                    IdLek = 3,
                    IdRecepta = 2,
                    Ilosc = "1 tabletka",
                    Dawkowanie = "1x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 4,
                    IdRecepta = 2,
                    Ilosc = "500 mg",
                    Dawkowanie = "2x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 5,
                    IdRecepta = 3,
                    Ilosc = "250 mg",
                    Dawkowanie = "3x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 6,
                    IdRecepta = 4,
                    Ilosc = "10 mg",
                    Dawkowanie = "1x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 7,
                    IdRecepta = 5,
                    Ilosc = "20 mg",
                    Dawkowanie = "1x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 3,
                    IdRecepta = 6,
                    Ilosc = "1 tabletka",
                    Dawkowanie = "2x dziennie"
                },
                new LekNaRecepcie
                {
                    IdLek = 1,
                    IdRecepta = 6,
                    Ilosc = "2 tabletki",
                    Dawkowanie = "4x dziennie"
                }
            });
        });
        
        //Konfiguracja Pielegniarek
        modelBuilder.Entity<Pielegniarka>(e =>
        {
            e.ToTable("Pielegniarki");
            
            e.HasBaseType<Osoba>();
            
            e.Property(e => e.NrPrawaWykonywaniaZawodu)
                .HasMaxLength(7)
                .IsRequired();

            e.Property(e => e.Grafik).IsRequired();
        
            e.HasIndex(e => e.NrPrawaWykonywaniaZawodu).IsUnique();

            e.HasData(new List<Pielegniarka>
            {
                new Pielegniarka
                {
                    IdOsoba = 9,
                    IdImion = 9,
                    Nazwisko = "Kowalska",
                    Pesel = "85010112345",
                    IdAdres = 9,
                    NrPrawaWykonywaniaZawodu = "6543210",
                    Grafik = "Poniedziałek-Piątek, 8:00-16:00",
                    NrTelefonu = "699 543 210"
                },
                new Pielegniarka
                {
                    IdOsoba = 10,
                    IdImion = 10,
                    Nazwisko = "Nowak",
                    Pesel = "86020267890",
                    IdAdres = 10,
                    NrPrawaWykonywaniaZawodu = "1234560",
                    Grafik = "Poniedziałek-Piątek, 9:00-17:00",
                    NrTelefonu = "609 876 321"
                }
            });
        });
    }
}