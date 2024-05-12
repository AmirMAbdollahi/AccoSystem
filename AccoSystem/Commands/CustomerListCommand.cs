namespace AccoSystem.Commands;

public class CustomerListCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("--------------------------------------------------\n");
        Console.WriteLine("1. New customer");
        Console.WriteLine("2. Edite customer");
        Console.WriteLine("3. Delete customer");
        Console.WriteLine("4. Update customer");
        Console.WriteLine("5. Search customer");
    }
}