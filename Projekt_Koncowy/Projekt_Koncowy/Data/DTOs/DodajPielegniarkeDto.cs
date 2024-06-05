﻿namespace Projekt_Koncowy.Data.DTOs;

public class DodajPielegniarkeDto
{
    public int IdImion { get; set; }
    public string Nazwisko { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public int IdAdres { get; set; }
    public string NrPrawaWykonywaniaZawodu { get; set; } = null!;
    public string Grafik { get; set; } = null!;
}