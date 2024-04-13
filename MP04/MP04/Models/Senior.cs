namespace MP04.Models;

public class Senior : KategoriaWiekowa
{
    public override void UmowWizyte()
    {
        Console.WriteLine("Senior - Umawiam wizytę w domu");
    }
}