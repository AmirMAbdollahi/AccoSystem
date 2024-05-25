using Microsoft.IdentityModel.Tokens;

namespace AccoSystem.Commands;

public class EditeCustomerCommand:Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to edite a customer.");
    }
    public void CustomerIndex()
    {
        Console.WriteLine("Please enter the number of the customer you want to edit ...");
    }
    public void Finish()
    {
        Console.WriteLine("Your customer edited .");
    }
    
}