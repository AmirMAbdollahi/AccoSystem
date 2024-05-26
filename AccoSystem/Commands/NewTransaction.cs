namespace AccoSystem.Commands;

public class NewTransaction : Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to register a new transaction.");
    }
    public void NewTypeId()
    {
        Console.WriteLine("please enter 1 or 2 for type :");
        Console.WriteLine("1 is income");
        Console.WriteLine("2 is const");
    }

    public void Finish()
    {
        Console.WriteLine("You added a new transaction.");
    }
}