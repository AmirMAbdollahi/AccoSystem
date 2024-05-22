using Microsoft.EntityFrameworkCore;

namespace AccoSystem.Commands;

public class TransactionsListCommand:ICommand
{
    public void Execute()
    {
        Console.WriteLine("--------------------------------------------------\n");
        Console.WriteLine("1. New transaction");
        Console.WriteLine("2. Edit transaction");
        Console.WriteLine("3. Delete transaction");
        Console.WriteLine("4. Search transaction");
    }
}


