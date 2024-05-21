namespace AccoSystem.Commands;

public class MainCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("1. Customer list ");
        Console.WriteLine("2. Transaction list");
        Console.WriteLine("3. Income report");
        Console.WriteLine("4. Cost report");
    }
}