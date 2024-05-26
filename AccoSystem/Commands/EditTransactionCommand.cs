using Microsoft.EntityFrameworkCore;

namespace AccoSystem.Commands;

public class EditTransactionCommand:Command
{
    public override void Execute()
    {
        Console.WriteLine("You want to edite a transaction ...");
    }

    public void TransactionIndex()
    {
        Console.WriteLine("Please enter the id of the transaction you want to edit ...");
    }
    public void Finish()
    {
        Console.WriteLine("Your transaction edited ...");
    }
    
}