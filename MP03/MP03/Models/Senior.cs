namespace MP03.Models;

// Dziedziczenie Wieloaspektowe - uwzględnia więcej niż 1 aspekt (kryterium podziału)
public class Senior : KategoriaWiekowa
{
    public override void UmowWizyte()
    {
        Console.WriteLine("Senior - Umawiam wizytę w domu");
    }
}