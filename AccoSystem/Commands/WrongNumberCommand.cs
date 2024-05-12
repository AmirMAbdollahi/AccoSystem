namespace AccoSystem.Commands;

public class WrongNumberCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("Please enter a valid number to continue....");
    }
}