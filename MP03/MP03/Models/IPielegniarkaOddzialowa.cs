namespace MP03.Models;

public interface IPielegniarkaOddzialowa
{
    string NumerPrawaWykonywaniaZawodu { get; set; }
    void PlanujGrafik();
}