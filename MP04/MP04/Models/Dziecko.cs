namespace MP04.Models;

public class Dziecko : KategoriaWiekowa
{
    public override void UmowWizyte()
    {
        Console.WriteLine("Dziecko - Umawiam wizytę w szkole");
    }
}