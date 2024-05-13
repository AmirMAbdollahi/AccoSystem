using System.Threading.Channels;

namespace AccoSystem.Commands;

public class DeleteCustomerCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("You want to Delete a customer.");
    }

    public void CustomerIndex()
    {
        Console.WriteLine("Please enter the number of the customer you want to delete ...");
    }

    public void Finish()
    {
        Console.WriteLine("The customer you wanted was deleted.");
    }
}