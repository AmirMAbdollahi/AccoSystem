namespace AccoSystem.Commands;

public class EditeCustomerCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to edite a customer.");
    }
    
    //Duplicate code
    public void CustomerIndex()
    {
        Console.WriteLine("Please enter the number of the customer you want to edite ...");
    }
    
    public void newFullName()
    {
        Console.WriteLine("Do you want to edite customer's name ? if not press enter.");
    }
    public void newEmail()
    {
        Console.WriteLine("Do you want to edite customer's email ? if not press enter.");
    }
    public void newMobile()
    {
        Console.WriteLine("Do you want to edite customer's mobile ? if not press enter.");
    }
    public void newAddrese()
    {
        Console.WriteLine("Do you want to edite customer's addrese ? if not press enter.");
    }
    
    public void finish()
    {
        Console.WriteLine("Your customer edited...");
    }
}