namespace AccoSystem.Commands;

public class MainCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("1. Customer list ");
        Console.WriteLine("2. 2");
        Console.WriteLine("3. 3");
        Console.WriteLine("4. 4");
    }
}