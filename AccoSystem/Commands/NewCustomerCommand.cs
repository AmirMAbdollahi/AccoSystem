namespace AccoSystem.Commands;

public class NewCustomerCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to create a new customer...");
        Console.WriteLine("Please fill in the requested items carefully.");
    }


    public void NewFullName()
    {
        Console.WriteLine("customer's full name is : ");
    }

    public void NewEmail()
    {
        Console.WriteLine("customer's email is : ");
    }

    public void NewMobile()
    {
        Console.WriteLine("customer's mobile is : ");
    }

    public void NewAddrese()
    {
        Console.WriteLine("customer's addrese is : ");
    }

    public void Finish()
    {
        Console.WriteLine("Your customer added...");
    }
}