namespace AccoSystem.Commands;

public class SearchTransaction : Command
{
    public override void Execute()
    {
        Console.WriteLine("You can search a transaction by date.");
    }


    public void FromDate()
    {
        Console.WriteLine("What date do you want to filter from?");
    }

    public void ToDate()
    {
        Console.WriteLine("Until what date do you want to filter?");
    }
}