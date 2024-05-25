namespace AccoSystem.Commands;

public class WrongNumberCommand:Command
{
    public override void Execute()
    {
        Console.WriteLine("Please enter a valid number to continue....");
    }
    
}