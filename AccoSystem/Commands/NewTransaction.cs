namespace AccoSystem.Commands;

public class NewTransaction:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to register a new transaction.");
    }
}