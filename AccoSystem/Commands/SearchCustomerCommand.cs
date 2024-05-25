namespace AccoSystem.Commands;

public class SearchCustomerCommand:Command
{
    public override void Execute()
    {
        Console.WriteLine("You can search a customer by name.");
        Console.WriteLine("Please enter the name you want ...");
    }
    
}