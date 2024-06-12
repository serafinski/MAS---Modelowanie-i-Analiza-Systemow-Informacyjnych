﻿using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data.Models;

namespace Projekt_Koncowy.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {
        
    }
    // ATRYBUT ZŁOŻONY - TABELA ADRES
    public DbSet<Adres> Adresy { get; set; }
    public DbSet<Doktor> Doktorzy { get; set; }
    public DbSet<Dorosly> Dorosli { get; set; }
    public DbSet<Dziecko> Dzieci { get; set; }
    public DbSet<Imiona> Imiona { get; set; }
    public DbSet<InterakcjaLeku> InterakcjeLekow { get; set; }
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
            //Klucz główny
            e.HasKey(p => p.IdPlacowka);
            // Nazwa placówki jest wymagana i ma max 255 znaków
            e.Property(p => p.Nazwa)
                .HasMaxLength(255)
                .IsRequired();
            
            // Placówka może mieć wielu kierowników, połączenie klucz obcy
            e.HasMany(p => p.Kierownicy)
                .WithOne(k => k.Placowka)
                .HasForeignKey(k => k.IdPlacowki);
            
            // W placówce może odbywać się wiele wizyt, połączenie klucz obcy
            e.HasMany(p => p.Wizyty)
                .WithOne(w => w.Placowka)
                .HasForeignKey(w => w.IdPlacowka);
            
            // KOMPOZYCJA 
            // Placówka może mieć wiele oddziałów, połączenie przez wymagany klucz obcy
            // Jeżeli placówka zostanie usunięta - wszystkie oddziały zostaną również usunięte
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
        
        //Konfiguracja oddział
        modelBuilder.Entity<Oddzial>(e =>
        {
            // Klucz główny
            e.HasKey(o => o.IdOddzial);
            // Nazwa oddziału jest wymagana i ma max 255 znaków
            e.Property(o => o.NazwaOddzial)
                .HasMaxLength(255)
                .IsRequired();
            
            // KOMPOZYCJA
            // Placówka może mieć wiele oddziałów, połączenie przez wymagany klucz obcy
            // Jeżeli placówka zostanie usunięta - wszystkie oddziały zostaną również usunięte
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
            // Klucz główny - będzie również używany przez inne klasy pochodne!
            e.HasKey(e => e.IdOsoba);
            
            // Generujemy ID osoby przy jej dodaniu
            e.Property(e => e.IdOsoba).ValueGeneratedOnAdd();
            
            // Nazwisko jest wymagane i może mieć max. 255 znaków
            e.Property(e => e.Nazwisko)
                .HasMaxLength(255)
                .IsRequired();
            
            // OGRANICZENIE ATRYBUTU
            // Nr telefonu może mieć max. 16 znaków
            e.Property(e => e.NrTelefonu)
                .HasMaxLength(16)
                .IsRequired();
            
            // PESEL jest wymagany i ma mieć dokładnie 11 znaków 
            e.Property(e => e.Pesel)
                .HasMaxLength(11)
                .IsRequired()
                .IsFixedLength();
            
            // OGRANICZENIE UNIQUE
            // Pesel musi być unikalny!
            e.HasIndex(e => e.Pesel).IsUnique();

            // Połączenie kluczem obcym z imionami
            // Osoba zostanie usunięta - usuń zestaw imion
            e.HasOne(e => e.Imiona)
                .WithMany(i => i.Osoby)
                .HasForeignKey(e => e.IdImion)
                .OnDelete(DeleteBehavior.Cascade);

            // Połączenie kluczem obcym z adresami
            // Osoba zostanie usunięta - usuń adres do niej przypisany
            e.HasOne(e => e.Adres)
                .WithMany(a => a.Osoby)
                .HasForeignKey(e => e.IdAdres)
                .OnDelete(DeleteBehavior.Cascade);
            
            //Brak przykładowych danych, bo pozostałe klasy dziedziczą po osobie - tam będzie definicja
        });

        //Konfiguracja dla Imiona
        modelBuilder.Entity<Imiona>(e =>
        {
            // Klucz główny
            e.HasKey(e => e.IdImiona);
            
            // Max. długośc 50, wymagane
            e.Property(e => e.PierwszeImie)
                .HasMaxLength(50)
                .IsRequired();

            // Max. długość 50, Osoba może nie mieć drugiego imienia!
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
        // ATRYBUT ZŁOŻONY - TABELA ADRES
        modelBuilder.Entity<Adres>(e =>
        {
            // Klucz główny
            e.HasKey(e => e.IdAdres);

            // Ulica jest wymagana, max. 255 znaków
            e.Property(e => e.Ulica)
                .HasMaxLength(255)
                .IsRequired();
            
            // Nr domu jest wymgany, max. 255 znaków
            e.Property(e => e.NrDomu)
                .HasMaxLength(10)
                .IsRequired();

            // Nr mieszkania nie jest wymagany, max. 6 znaków
            e.Property(e => e.NrMieszkania)
                .HasMaxLength(6);

            // Kod pocztowy jest wymagany, max. 6 znaków
            e.Property(e => e.KodPocztowy)
                .HasMaxLength(6)
                .IsRequired();

            // Miejscowość jest wymagana, max. 255 znaków
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
            // Do tabeli Doktorzy
            e.ToTable("Doktorzy");
        
            // Bazowy typ Osoba
            e.HasBaseType<Osoba>();
        
            // Nr. prawa wykonywania zawodu jest wymagany - max. 7 znaków
            e.Property(e => e.NrPrawaWykonywaniaZawodu)
                .HasMaxLength(7)
                .IsRequired();
        
            // Nr. prawa wykonywania zawodu jest wymagany musi być unikalny!
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
            // Typ bazowy Doktor - by być kierownikem musi być doktorem 
            e.HasBaseType<Doktor>();

            //Data objęcia stanowiska jest wymagana
            e.Property(k => k.DataObjeciaStanowiska).IsRequired();
            
            // Powiązanie kluczem obcym z Placówką - kierownik kieruje placówką 
            // By usunąć placówkę - trzeba usunąć kierownika
            e.HasOne(k => k.Placowka)
                .WithMany(p => p.Kierownicy)
                .HasForeignKey(k => k.IdPlacowki)
                .OnDelete(DeleteBehavior.Restrict);

            // Indeks dla kierownika w danej placowce - musi być unkatowy
            // Kierownik nie może kierować 2 placówkami - to to zapewnia
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
            // Do tabeli Pacjenci 
            e.ToTable("Pacjenci");
            
            // Typ bazowy Osoba - dziedziczenie z klasy abstrakcyjnej Osoba
            e.HasBaseType<Osoba>();
            
            // Pacjent 
            e.Property(e => e.NrKontaktuAlarmowego)
                .HasMaxLength(16)
                .IsRequired();
            
            // Brak danych tutaj bo Pacjent dzieli się na Dorosłego, Dziecko i Seniora
        });
        
        //Konfiguracja Dorosly
        modelBuilder.Entity<Dorosly>(e =>
        {
            // NIP pracodawcy jest wymagany, max. 11 znaków
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
            // Rok przejścia na emeryturę jest wymagany, max. 4 znaki
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
                    Pesel = "63010112345",
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
            // Nazwa szkoły jest wymagana, max. 255 znaków
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
                    Pesel = "14301898266",
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
                    Pesel = "13241132878",
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
            // Klucz główny
            e.HasKey(e => e.IdWizyty);

            // Opis wizyty jest wymagany
            e.Property(e => e.OpisWizyty).IsRequired();

            // Połączenie kluczem obcym z Pacjentem,
            // Usunięcie wizyty nie usuwa Pacjenta
            e.HasOne(e => e.Pacjent)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdPacjent)
                .OnDelete(DeleteBehavior.NoAction);
            
            // ASOCJACJA ZWYKŁA
            // Połączenie kluczem obcym z Doktorem,
            // Usunięcie wizyty nie usuwa Doktora
            e.HasOne(e => e.Doktor)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdDoktor)
                .OnDelete(DeleteBehavior.NoAction);

            // Połączenie kluczem obcym z Placówką,
            // Usunięcie wizyty nie usuwa Placówki
            e.HasOne(e => e.Placowka)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(e => e.IdPlacowka)
                .OnDelete(DeleteBehavior.NoAction);
            
            // Pole do asocjacji kwalifikowanej
            e.Property(e => e.NrWizyty)
                .HasMaxLength(10)
                .IsRequired();
            
            // NrWizyty musi być unikatowy
            e.HasIndex(e => e.NrWizyty).IsUnique();
            
            //Doktor z id 1 -> 9 wizyt do pokazania wlasnego ograniczenia!
            e.HasData(new List<Wizyta>
            {
                new Wizyta
                {
                    IdWizyty = 1,
                    DataWizyty = new DateTime(2024, 6, 2),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 2,
                    IdPlacowka = 1,
                    NrWizyty = "ABC123"
                },
                new Wizyta
                {
                    IdWizyty = 2,
                    DataWizyty = new DateTime(2024, 6, 3),
                    OpisWizyty = "Wizyta specjalistyczna",
                    IdDoktor = 1,
                    IdPacjent = 2,
                    IdPlacowka = 2,
                    NrWizyty = "DEF456"
                },
                new Wizyta
                {
                    IdWizyty = 3,
                    DataWizyty = new DateTime(2024, 6, 4),
                    OpisWizyty = "Wizyta rutynowa",
                    IdDoktor = 1,
                    IdPacjent = 3,
                    IdPlacowka = 1,
                    NrWizyty = "GHI789"
                },
                new Wizyta
                {
                    IdWizyty = 4,
                    DataWizyty = new DateTime(2024, 6, 5),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 7,
                    IdPlacowka = 1,
                    NrWizyty = "JKL012"
                },
                new Wizyta
                {
                    IdWizyty = 5,
                    DataWizyty = new DateTime(2024, 6, 6),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 8,
                    IdPlacowka = 2,
                    NrWizyty = "MNO345"
                },
                new Wizyta
                {
                    IdWizyty = 6,
                    DataWizyty = new DateTime(2024, 6, 7),
                    OpisWizyty = "Wizyta specjalistyczna",
                    IdDoktor = 1,
                    IdPacjent = 5,
                    IdPlacowka = 1,
                    NrWizyty = "PQR678"
                },
                new Wizyta
                {
                    IdWizyty = 7,
                    DataWizyty = new DateTime(2024, 6, 8),
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 1,
                    IdPacjent = 6,
                    IdPlacowka = 2,
                    NrWizyty = "STU901"
                },
                new Wizyta
                {
                    IdWizyty = 8,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 2,
                    IdPlacowka = 1,
                    NrWizyty = "VWX234"
                },
                new Wizyta
                {
                    IdWizyty = 9,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta specjalistyczna",
                    IdDoktor = 4,
                    IdPacjent = 3,
                    IdPlacowka = 1,
                    NrWizyty = "YZA567"
                },
                new Wizyta
                {
                    IdWizyty = 10,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta rutynowa",
                    IdDoktor = 4,
                    IdPacjent = 7,
                    IdPlacowka = 1,
                    NrWizyty = "BCD890"
                },
                new Wizyta
                {
                    IdWizyty = 11,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 8,
                    IdPlacowka = 1,
                    NrWizyty = "EFG123"
                },
                new Wizyta
                {
                    IdWizyty = 12,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 5,
                    IdPlacowka = 1,
                    NrWizyty = "HIJ456"
                },
                new Wizyta
                {
                    IdWizyty = 13,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta specjalistyczna",
                    IdDoktor = 4,
                    IdPacjent = 6,
                    IdPlacowka = 1,
                    NrWizyty = "KLM789"
                },
                new Wizyta
                {
                    IdWizyty = 14,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 2,
                    IdPlacowka = 1,
                    NrWizyty = "NOP012"
                },
                new Wizyta
                {
                    IdWizyty = 15,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 3,
                    IdPlacowka = 1,
                    NrWizyty = "QRS345"
                },
                new Wizyta
                {
                    IdWizyty = 16,
                    DataWizyty = DateTime.Now,
                    OpisWizyty = "Wizyta kontrolna",
                    IdDoktor = 4,
                    IdPacjent = 7,
                    IdPlacowka = 1,
                    NrWizyty = "TUV678"
                }
            });
        });

        //Konfiguracja Lek
        modelBuilder.Entity<Lek>(e =>
        {
            // Klucz główny
            e.HasKey(e => e.IdLek);
            
            // Nazwa leku jest wymagana, max. 255 znaków
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
        
        // Konfiguracja interakcji leków
        modelBuilder.Entity<InterakcjaLeku>(e =>
        {
            // Klucz główny - kombinacja interakcji musi być unikalna
            e.HasKey(e => new { e.IdLek1, e.IdLek2 });

            // Powiązanie kluczem obcym z Lek1
            // Nie można usunąć leku jeżeli jest on na liście interakcji
            e.HasOne(e => e.Lek1)
                .WithMany()
                .HasForeignKey(e => e.IdLek1)
                .OnDelete(DeleteBehavior.Restrict);

            // Powiązanie kluczem obcym z Lek2
            // Nie można usunąć leku jeżeli jest on na liście interakcji
            e.HasOne(e => e.Lek2)
                .WithMany()
                .HasForeignKey(e => e.IdLek2)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasData(new List<InterakcjaLeku>
            {
                new InterakcjaLeku { IdLek1 = 5, IdLek2 = 6 }
            });
        });
        
        //Konfiguracja Recepta
        modelBuilder.Entity<Recepta>(e =>
        {
            // Klucz główny
            e.HasKey(e => e.IdRecepta);
            
            // Data wystawienia recepty jest wymagana
            e.Property(e => e.DataWystawienia)
                .IsRequired();
            
            // Na jednej wizycie może być wiele recept,
            // Usunięcie wizyty spowoduje usunięcie recepty
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
            // Klucz główny - kombinacja leku i recepty musi być unikalna
            e.HasKey(e => new { e.IdLek, e.IdRecepta });
            
            // Ilość jest wymagana, max. 255 znaków
            e.Property(e => e.Ilosc)
                .HasMaxLength(255)
                .IsRequired();
            
            // Dawkowanie jest wymagane, max. 255 znaków
            e.Property(e => e.Dawkowanie)
                .HasMaxLength(255)
                .IsRequired();
            
            //ASOCJACJA Z ATRYBUTEM
            
            // Powiązanie z lekiem
            // Jeżeli lek zostanie usunięty - usunie LekNaRecepcie
            e.HasOne(e => e.Lek)
                .WithMany(l => l.LekiNaRecepcie)
                .HasForeignKey(e => e.IdLek)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Powiązanie z receptą
            // Jeżeli recepta zostanie usunięta - usunie LekNaRecepcie
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
            // Do tabeli Pielegniarki
            e.ToTable("Pielegniarki");
            
            // Podstawowy typ Osoba
            e.HasBaseType<Osoba>();
            
            // Nr. prawa wykonywania zawodu jest wymagany - max. 7 znaków
            e.Property(e => e.NrPrawaWykonywaniaZawodu)
                .HasMaxLength(7)
                .IsRequired();
            
            // Nr. prawa wykonywania zawodu musi być unikalny
            e.HasIndex(e => e.NrPrawaWykonywaniaZawodu).IsUnique();
            
            // Grafik jest wymagany
            e.Property(e => e.Grafik).IsRequired();

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