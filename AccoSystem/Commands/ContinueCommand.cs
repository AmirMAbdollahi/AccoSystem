namespace AccoSystem.Commands;

public class ContinueCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("Enter one of the numbers to continue....");
    }
}