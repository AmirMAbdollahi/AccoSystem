namespace AccoSystem.Commands;

public class NewTransaction:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to register a new transaction.");
    }

    public void NewAmount()
    {
        Console.WriteLine("please enter amount :");
    }
    public void NewDescription()
    {
        Console.WriteLine("please enter Description :");
    }
    public void NewTypeId()
    {
        Console.WriteLine("please enter 1 or 2 for type :");
        Console.WriteLine("1 is income");
        Console.WriteLine("2 is const");
    }
}