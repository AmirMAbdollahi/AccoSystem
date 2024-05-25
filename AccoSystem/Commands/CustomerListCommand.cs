namespace AccoSystem.Commands;

public class CustomerListCommand:Command
{
    public override void Execute()
    {
        Console.WriteLine("--------------------------------------------------\n");
        Console.WriteLine("1. New customer");
        Console.WriteLine("2. Edit customer");
        Console.WriteLine("3. Delete customer");
        Console.WriteLine("4. Search customer");
    }
}