namespace AccoSystem.Commands;

public class WelcomeCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("Hello welcome to my console app.");
    }
}