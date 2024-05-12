namespace AccoSystem.Commands;

public class NewCustomerCommand:ICommand
{
    public string _fullName { get; set; }
    public string _email { get; set; }
    public string _mobile { get; set; }
    public string _addrese { get; set; }
    
    public void Execute()
    {
        Console.WriteLine("You want to create a new customer...");
        Console.WriteLine("Please fill in the requested items carefully.");
    }

    public void newFullName()
    {
        Console.WriteLine($"customer's full name is : {_fullName}");
    }
    public void newEmail()
    {
        Console.WriteLine($"customer's email is : {_email}");
    }
    public void newMobile()
    {
        Console.WriteLine($"customer's mobile is : {_mobile}");
    }
    public void newAddrese()
    {
        Console.WriteLine($"customer's addrese is : {_addrese}");
    }

    public void finish()
    {
        Console.WriteLine("Your customer added...");
    }
}