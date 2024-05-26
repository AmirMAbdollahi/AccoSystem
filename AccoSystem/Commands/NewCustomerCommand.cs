namespace AccoSystem.Commands;

public class NewCustomerCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to create a new customer...");
        Console.WriteLine("Please fill in the requested items carefully.");
    }
    public void Finish()
    {
        Console.WriteLine("Your customer added...");
    }
}