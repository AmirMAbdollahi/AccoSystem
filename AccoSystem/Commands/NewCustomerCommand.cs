namespace AccoSystem.Commands;

public class NewCustomerCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to create a new customer...");
        Console.WriteLine("Please fill in the requested items carefully.");
    }

    public void newFullName()
    {
        Console.WriteLine("customer's full name is : ");
    }
    public void newEmail()
    {
        Console.WriteLine("customer's email is : ");
    }
    public void newMobile()
    {
        Console.WriteLine("customer's mobile is : ");
    }
    public void newAddrese()
    {
        Console.WriteLine("customer's addrese is : ");
    }

    public void finish()
    {
        Console.WriteLine("Your customer added...");
    }
}