﻿namespace MP02.Models;

public class Oddzial
{
    public int IdOddzialu { get; set; }
    public string NazwaOddzialu { get; set; }
    public Szpital Szpital { get; set; }

    public Oddzial(int id, string nazwaOddzialu, Szpital szpital)
    {
        IdOddzialu = id;

        if (nazwaOddzialu is null)
        {
            throw new ArgumentException("Oddział musi posiadać nazwę!");
        }
        NazwaOddzialu = nazwaOddzialu;
        
        // Wymaganie by istniał szpital by móc dodać oddział - Kompozycja
        if (szpital is null)
        {
            throw new ArgumentException("Oddział musi posiadać szpital!!");
        }
        Szpital = szpital;
    }
}