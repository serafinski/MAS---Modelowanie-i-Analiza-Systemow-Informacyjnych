﻿namespace Projekt_Koncowy.Data.DTOs;

public class DoktorWyswietlDto
{
    public int IdDoktor { get; set; }
    public string Imie { get; set; } = null!;
    public string? DrugieImie { get; set; }
    public string Nazwisko { get; set; } = null!;
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    public string NrTelefonu { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public string Ulica { get; set; } = null!;
    public string NrDomu { get; set; } = null!;
    public int? NrMieszkania { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string Miejscowosc { get; set; } = null!;
}